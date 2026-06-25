using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;

namespace DynamicDumpDec;

internal static class Program
{
    private const int BaudRate = 4800;
    private const int SampleSize = 5;
    private const int ReadTimeoutMs = 2000;
    private const int WriteTimeoutMs = 500;
    private const int DefaultSampleCount = 240;
    private const int WarmupSampleCount = 2;
    private const int AuxOffset = 28;
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

        if (!TryParseArgs(args, out var portName, out var sampleCount))
        {
            PrintUsage();
            WaitBeforeExit();
            return 2;
        }

        try
        {
            Dump(portName, sampleCount);
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

    private static void Dump(string portName, int sampleCount)
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

        PrintHeader(portName, sampleCount);

        var sample = new byte[SampleSize];
        var bytesTotal = 0;
        var ch0Min = int.MaxValue;
        var ch0Max = int.MinValue;
        var ch1Min = int.MaxValue;
        var ch1Max = int.MinValue;
        var auxMin = int.MaxValue;
        var auxMax = int.MinValue;
        long ch0Sum = 0;
        long ch1Sum = 0;
        long auxSum = 0;
        var auxOkCount = 0;
        var auxBadCount = 0;
        var auxOkAfterWarmupCount = 0;
        var auxBadAfterWarmupCount = 0;
        var auxCounts = new int[256];
        var sw = Stopwatch.StartNew();

        sp.Write(new[] { ReqByte }, 0, 1);

        for (var index = 0; index < sampleCount; index++)
        {
            ReadExact(sp, sample, SampleSize);
            bytesTotal += SampleSize;

            var timeMs = (int)Math.Round(sw.Elapsed.TotalMilliseconds);
            var ch0 = ReadUInt16Le(sample, 0);
            var ch1 = ReadUInt16Le(sample, 2);
            var aux = sample[4];
            var auxExpected = sample[0] + sample[2] + AuxOffset;
            var auxOk = aux == auxExpected;
            var isWarmup = index < WarmupSampleCount;

            ch0Min = Math.Min(ch0Min, ch0);
            ch0Max = Math.Max(ch0Max, ch0);
            ch1Min = Math.Min(ch1Min, ch1);
            ch1Max = Math.Max(ch1Max, ch1);
            auxMin = Math.Min(auxMin, aux);
            auxMax = Math.Max(auxMax, aux);
            ch0Sum += ch0;
            ch1Sum += ch1;
            auxSum += aux;
            auxCounts[aux]++;
            if (auxOk)
            {
                auxOkCount++;
                if (!isWarmup) auxOkAfterWarmupCount++;
            }
            else
            {
                auxBadCount++;
                if (!isWarmup) auxBadAfterWarmupCount++;
            }

            Console.WriteLine(
                $"{index:000} {timeMs:000000} {FormatSample(sample)} {ch0:00000} {ch1:00000} {aux:000} {auxExpected:000} {FormatBool(auxOk)} {FormatSampleKind(isWarmup)}");
        }

        sw.Stop();
        var elapsedSec = Math.Max(sw.Elapsed.TotalSeconds, 0.001);
        var hz = sampleCount / elapsedSec;

        Console.WriteLine("SUMMARY");
        Console.WriteLine($"BYTES={bytesTotal}");
        Console.WriteLine($"SAMPLES={sampleCount}");
        Console.WriteLine("TAIL_BYTES=0");
        Console.WriteLine($"HZ={hz.ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MIN={ch0Min}");
        Console.WriteLine($"CH0_AVG={(ch0Sum / (double)sampleCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MAX={ch0Max}");
        Console.WriteLine($"CH1_MIN={ch1Min}");
        Console.WriteLine($"CH1_AVG={(ch1Sum / (double)sampleCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH1_MAX={ch1Max}");
        Console.WriteLine($"AUX_MIN={auxMin}");
        Console.WriteLine($"AUX_AVG={(auxSum / (double)sampleCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"AUX_MAX={auxMax}");
        Console.WriteLine($"AUX_COUNTS={FormatAuxCounts(auxCounts)}");
        Console.WriteLine($"AUX_OK_COUNT={auxOkCount}");
        Console.WriteLine($"AUX_BAD_COUNT={auxBadCount}");
        Console.WriteLine($"AUX_OK_AFTER_WARMUP={auxOkAfterWarmupCount}");
        Console.WriteLine($"AUX_BAD_AFTER_WARMUP={auxBadAfterWarmupCount}");
    }

    private static int ReadUInt16Le(byte[] data, int offset)
        => data[offset + 1] * 256 + data[offset];

    private static string FormatSample(byte[] data)
        => string.Join(" ", Array.ConvertAll(data, b => b.ToString("000", CultureInfo.InvariantCulture)));

    private static string FormatBool(bool value)
        => value ? "OK" : "BAD";

    private static string FormatSampleKind(bool isWarmup)
        => isWarmup ? "WARMUP" : "DATA";

    private static string FormatAuxCounts(int[] counts)
    {
        var parts = new List<string>();
        for (var value = 0; value < counts.Length; value++)
        {
            if (counts[value] > 0)
                parts.Add($"{value:000}:{counts[value]}");
        }

        return string.Join(",", parts);
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

    private static void PrintHeader(string portName, int sampleCount)
    {
        Console.WriteLine($"MODE={ModeName}");
        Console.WriteLine($"PORT={portName}");
        Console.WriteLine($"SET={SetByte}");
        Console.WriteLine($"REQ={ReqByte}");
        Console.WriteLine("REQUEST_MODE=STREAM_AFTER_SINGLE_REQ");
        Console.WriteLine($"SAMPLES_TARGET={sampleCount}");
        Console.WriteLine($"SAMPLE_SIZE={SampleSize}");
        Console.WriteLine($"WARMUP_SAMPLES={WarmupSampleCount}");
        Console.WriteLine("FORMAT=SAMPLE5_UINT16_LE");
        Console.WriteLine("SAMPLE=CH0_LO CH0_HI CH1_LO CH1_HI AUX");
        Console.WriteLine($"AUX_FORMULA=B0+B2+{AuxOffset}");
        Console.WriteLine("IDX TIME_MS B0 B1 B2 B3 B4 CH0 CH1 AUX AUX_EXPECTED AUX_OK KIND");
    }

    private static bool TryParseArgs(string[] args, out string portName, out int sampleCount)
    {
        portName = args.Length >= 1 ? args[0].Trim() : DefaultPort;
        sampleCount = DefaultSampleCount;

        if (args.Length > 2)
            return false;

        if (args.Length >= 2 && (!int.TryParse(args[1], out sampleCount) || sampleCount <= 0))
            return false;

        return !string.IsNullOrWhiteSpace(portName);
    }

    private static void PrintUsage()
    {
        Console.Error.WriteLine("USAGE=DynamicDumpDec COM3 240");
    }

    private static void WaitBeforeExit()
    {
        Console.WriteLine("DONE=PRESS_ENTER_TO_EXIT");
        Console.ReadLine();
    }
}
