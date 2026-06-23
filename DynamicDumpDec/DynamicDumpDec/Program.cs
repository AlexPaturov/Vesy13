using System;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Threading;

namespace DynamicDumpDec;

internal static class Program
{
    private const int BaudRate = 4800;
    private const int PacketSize = 4;
    private const int ReadTimeoutMs = 2000;
    private const int WriteTimeoutMs = 500;
    private const int DefaultPacketCount = 60;
    private const string DefaultPort = "COM3";
    private const string ModeName = "DYNAMIC";
    private const byte SetByte = 126;
    private const byte ReqByte = 254;

    private static int Main(string[] args)
    {
        if (!TryParseArgs(args, out var portName, out var packetCount))
        {
            PrintUsage();
            return 2;
        }

        try
        {
            Dump(portName, packetCount);
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERROR={ex.GetType().Name}");
            Console.Error.WriteLine($"MESSAGE={ex.Message}");
            return 1;
        }
    }

    private static void Dump(string portName, int packetCount)
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

        PrintHeader(portName, packetCount);

        var buffer = new byte[PacketSize];
        var bytesTotal = 0;
        var ch0Min = int.MaxValue;
        var ch0Max = int.MinValue;
        var ch1Min = int.MaxValue;
        var ch1Max = int.MinValue;
        long ch0Sum = 0;
        long ch1Sum = 0;
        var sw = Stopwatch.StartNew();

        sp.Write(new[] { ReqByte }, 0, 1);

        for (var index = 0; index < packetCount; index++)
        {
            ReadExact(sp, buffer, PacketSize);
            bytesTotal += PacketSize;

            var ch0 = buffer[1] * 256 + buffer[0];
            var ch1 = buffer[3] * 256 + buffer[2];
            var timeMs = (int)Math.Round(sw.Elapsed.TotalMilliseconds);

            ch0Min = Math.Min(ch0Min, ch0);
            ch0Max = Math.Max(ch0Max, ch0);
            ch1Min = Math.Min(ch1Min, ch1);
            ch1Max = Math.Max(ch1Max, ch1);
            ch0Sum += ch0;
            ch1Sum += ch1;

            Console.WriteLine(
                $"{index:000} {timeMs:000000} {buffer[0]:000} {buffer[1]:000} {buffer[2]:000} {buffer[3]:000} {ch0:00000} {ch1:00000}");
        }

        sw.Stop();
        var elapsedSec = Math.Max(sw.Elapsed.TotalSeconds, 0.001);
        var hz = packetCount / elapsedSec;

        Console.WriteLine("SUMMARY");
        Console.WriteLine($"BYTES={bytesTotal}");
        Console.WriteLine($"PACKETS={packetCount}");
        Console.WriteLine("TAIL_BYTES=0");
        Console.WriteLine($"HZ={hz.ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MIN={ch0Min}");
        Console.WriteLine($"CH0_AVG={(ch0Sum / (double)packetCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MAX={ch0Max}");
        Console.WriteLine($"CH1_MIN={ch1Min}");
        Console.WriteLine($"CH1_AVG={(ch1Sum / (double)packetCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH1_MAX={ch1Max}");
    }

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

    private static void PrintHeader(string portName, int packetCount)
    {
        Console.WriteLine($"MODE={ModeName}");
        Console.WriteLine($"PORT={portName}");
        Console.WriteLine($"SET={SetByte}");
        Console.WriteLine($"REQ={ReqByte}");
        Console.WriteLine("REQUEST_MODE=STREAM_AFTER_SINGLE_REQ");
        Console.WriteLine($"PACKETS_TARGET={packetCount}");
        Console.WriteLine($"PACKET_SIZE={PacketSize}");
        Console.WriteLine("FORMAT=CH0_UINT16_LE_CH1_UINT16_LE");
        Console.WriteLine("IDX TIME_MS B0 B1 B2 B3 CH0 CH1");
    }

    private static bool TryParseArgs(string[] args, out string portName, out int packetCount)
    {
        portName = args.Length >= 1 ? args[0].Trim() : DefaultPort;
        packetCount = DefaultPacketCount;

        if (args.Length > 2)
            return false;

        if (args.Length >= 2 && (!int.TryParse(args[1], out packetCount) || packetCount <= 0))
            return false;

        return !string.IsNullOrWhiteSpace(portName);
    }

    private static void PrintUsage()
    {
        Console.Error.WriteLine("USAGE=DynamicDumpDec COM3 60");
    }
}
