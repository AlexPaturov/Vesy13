using System;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Threading;

namespace DynamicDumpDec;

internal static class Program
{
    private const int BaudRate = 4800;
    private const int BlockSize = 20;
    private const int ReadTimeoutMs = 2000;
    private const int WriteTimeoutMs = 500;
    private const int DefaultBlockCount = 60;
    private const string DefaultPort = "COM3";
    private const string ModeName = "DYNAMIC";
    private const byte SetByte = 126;
    private const byte ReqByte = 254;

    private static int Main(string[] args)
    {
        var exitCode = 0;

        Console.WriteLine("Press Enter to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
        }

        if (!TryParseArgs(args, out var portName, out var blockCount))
        {
            PrintUsage();
            WaitBeforeExit();
            return 2;
        }

        try
        {
            Dump(portName, blockCount);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERROR={ex.GetType().Name}");
            Console.Error.WriteLine($"MESSAGE={ex.Message}");
            exitCode = 1;
        }

        WaitBeforeExit();
        return exitCode;
    }

    private static void Dump(string portName, int blockCount)
    {
        using var sp = new SerialPort
        {
            PortName = portName,
            BaudRate = BaudRate,
            DataBits = 8,
            Parity = Parity.Even,
            StopBits = StopBits.One,
            ReadTimeout = ReadTimeoutMs,
            WriteTimeout = WriteTimeoutMs
        };

        sp.Open();
        sp.DiscardInBuffer();
        sp.DiscardOutBuffer();
        sp.Write(new[] { SetByte }, 0, 1);
        Thread.Sleep(50);

        PrintHeader(portName, blockCount);

        var block = new byte[BlockSize];
        var bytesTotal = 0;
        var aCh0Min = int.MaxValue;
        var aCh0Max = int.MinValue;
        var aCh1Min = int.MaxValue;
        var aCh1Max = int.MinValue;
        var bCh0Min = int.MaxValue;
        var bCh0Max = int.MinValue;
        var bCh1Min = int.MaxValue;
        var bCh1Max = int.MinValue;
        long aCh0Sum = 0;
        long aCh1Sum = 0;
        long bCh0Sum = 0;
        long bCh1Sum = 0;
        var sw = Stopwatch.StartNew();

        sp.Write(new[] { ReqByte }, 0, 1);

        for (var index = 0; index < blockCount; index++)
        {
            ReadExact(sp, block, BlockSize);
            bytesTotal += BlockSize;

            var timeMs = (int)Math.Round(sw.Elapsed.TotalMilliseconds);
            var aCh0 = ReadUInt16Le(block, 0);
            var aCh1 = ReadUInt16Le(block, 2);
            var bCh0 = ReadUInt16Le(block, 10);
            var bCh1 = ReadUInt16Le(block, 12);

            aCh0Min = Math.Min(aCh0Min, aCh0);
            aCh0Max = Math.Max(aCh0Max, aCh0);
            aCh1Min = Math.Min(aCh1Min, aCh1);
            aCh1Max = Math.Max(aCh1Max, aCh1);
            bCh0Min = Math.Min(bCh0Min, bCh0);
            bCh0Max = Math.Max(bCh0Max, bCh0);
            bCh1Min = Math.Min(bCh1Min, bCh1);
            bCh1Max = Math.Max(bCh1Max, bCh1);
            aCh0Sum += aCh0;
            aCh1Sum += aCh1;
            bCh0Sum += bCh0;
            bCh1Sum += bCh1;

            Console.WriteLine(
                $"{index:000} {timeMs:000000} {FormatBlock(block)} {aCh0:00000} {aCh1:00000} {bCh0:00000} {bCh1:00000}");
        }

        sw.Stop();
        var elapsedSec = Math.Max(sw.Elapsed.TotalSeconds, 0.001);
        var hz = blockCount / elapsedSec;

        Console.WriteLine("SUMMARY");
        Console.WriteLine($"BYTES={bytesTotal}");
        Console.WriteLine($"BLOCKS={blockCount}");
        Console.WriteLine("TAIL_BYTES=0");
        Console.WriteLine($"HZ={hz.ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"A_CH0_MIN={aCh0Min}");
        Console.WriteLine($"A_CH0_AVG={(aCh0Sum / (double)blockCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"A_CH0_MAX={aCh0Max}");
        Console.WriteLine($"A_CH1_MIN={aCh1Min}");
        Console.WriteLine($"A_CH1_AVG={(aCh1Sum / (double)blockCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"A_CH1_MAX={aCh1Max}");
        Console.WriteLine($"B_CH0_MIN={bCh0Min}");
        Console.WriteLine($"B_CH0_AVG={(bCh0Sum / (double)blockCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"B_CH0_MAX={bCh0Max}");
        Console.WriteLine($"B_CH1_MIN={bCh1Min}");
        Console.WriteLine($"B_CH1_AVG={(bCh1Sum / (double)blockCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"B_CH1_MAX={bCh1Max}");
    }

    private static int ReadUInt16Le(byte[] data, int offset)
        => data[offset + 1] * 256 + data[offset];

    private static string FormatBlock(byte[] data)
        => string.Join(" ", Array.ConvertAll(data, b => b.ToString("000", CultureInfo.InvariantCulture)));

    private static void ReadExact(SerialPort sp, byte[] buffer, int count)
    {
        var offset = 0;
        while (offset < count)
        {
            var read = sp.Read(buffer, offset, count - offset);
            if (read <= 0) throw new TimeoutException("COM read returned no data.");
            offset += read;
        }
    }

    private static void PrintHeader(string portName, int blockCount)
    {
        Console.WriteLine($"MODE={ModeName}");
        Console.WriteLine($"PORT={portName}");
        Console.WriteLine($"SET={SetByte}");
        Console.WriteLine($"REQ={ReqByte}");
        Console.WriteLine("REQUEST_MODE=STREAM_AFTER_SINGLE_REQ");
        Console.WriteLine($"BLOCKS_TARGET={blockCount}");
        Console.WriteLine($"BLOCK_SIZE={BlockSize}");
        Console.WriteLine("FORMAT=BLOCK20_UINT16_LE");
        Console.WriteLine("A=BYTES_00_03  B=BYTES_10_13");
        Console.WriteLine("IDX TIME_MS B00 B01 B02 B03 B04 B05 B06 B07 B08 B09 B10 B11 B12 B13 B14 B15 B16 B17 B18 B19 A_CH0 A_CH1 B_CH0 B_CH1");
    }

    private static bool TryParseArgs(string[] args, out string portName, out int blockCount)
    {
        portName = args.Length >= 1 ? args[0].Trim() : DefaultPort;
        blockCount = DefaultBlockCount;

        if (args.Length > 2)
            return false;

        if (args.Length >= 2 && (!int.TryParse(args[1], out blockCount) || blockCount <= 0))
            return false;

        return !string.IsNullOrWhiteSpace(portName);
    }

    private static void PrintUsage()
    {
        Console.Error.WriteLine("USAGE=DynamicDumpDec COM3 60");
    }

    private static void WaitBeforeExit()
    {
        Console.WriteLine("DONE=PRESS_ENTER_TO_EXIT");
        Console.ReadLine();
    }
}
