using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using Vesy13.Protocol;

namespace DynamicDumpDec;

internal static class Program
{
    private const int BaudRate = 4800;
    private const int IncommingMessageSize = 5;
    private const int ReadTimeoutMs = 2000;
    private const int WriteTimeoutMs = 500;
    private const int DefaultSampleCount = 40;
    private const string DefaultPort = "COM1";
    private const string ModeName = "DYNAMIC";

    private const byte Avg1SetByte = 14;
    private const byte Avg1ReqByte = 142;
    private const byte Avg2SetByte = 30;
    private const byte Avg2ReqByte = 158;
    private const byte Avg4SetByte = 46;
    private const byte Avg4ReqByte = 174;
    private const byte Avg8SetByte = 62;
    private const byte Avg8ReqByte = 190;
    private const byte Avg16SetByte = 78;
    private const byte Avg16ReqByte = 206;
    private const byte Avg32SetByte = 94;
    private const byte Avg32ReqByte = 222;
    private const byte Avg64SetByte = 110;
    private const byte Avg64ReqByte = 238;
    private const byte Avg128SetByte = 126;
    private const byte Avg128ReqByte = 254;

    private const byte SetByte = Avg128SetByte;
    private const byte ReqByte = Avg128ReqByte;

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

    private static void Dump(string portName, int TargetFrameCount)
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
        sp.Write(new[] { SetByte }, 0, 1);          // установили режим работы - динамика
        Thread.Sleep(50);

        PrintHeader(portName, TargetFrameCount);
        #region set: var's
        var incomFrame = new byte[IncommingMessageSize];
        var sampleBytes = 0;
        var rawBytes = 0;
        var skippedBytes = 0;
        var ch0Min = int.MaxValue;
        var ch0Max = int.MinValue;
        var ch1Min = int.MaxValue;
        var ch1Max = int.MinValue;
        var auxMin = int.MaxValue;
        var auxMax = int.MinValue;
        long ch0Sum = 0;
        long ch1Sum = 0;
        long auxSum = 0;
        var auxCounts = new int[256];
        var sw = Stopwatch.StartNew();
        #endregion

        sp.Write(new[] { ReqByte }, 0, 1);                          // запустили прием потока

        for (var index = 0; index < TargetFrameCount;)
        {
            AddByte(incomFrame, ref sampleBytes, ReadByte(sp));     // 
            rawBytes++;

            if (sampleBytes < IncommingMessageSize)                 // пока меньше 5-ти крутим цикл
                continue;

            var frame = SimA04DynamicFrame.Parse(incomFrame);

            switch (frame.State)
            {
                case FrameState.InvalidChecksum:
                    ShiftLeft(incomFrame, ref sampleBytes);
                    skippedBytes++;
                    continue;

                case FrameState.Valid:
                    break;

                default:
                    throw new InvalidOperationException($"Unexpected frame state: {frame.State}");
            }

            var timeMs = (int)Math.Round(sw.Elapsed.TotalMilliseconds);
            
            //var ch0 = ReadUInt16Le(incomFrame, 0);
            //var ch1 = ReadUInt16Le(incomFrame, 2);
            //var aux = incomFrame[4];

            #region Footer counters
            ch0Min = Math.Min(ch0Min, frame.Ch0!.Value);
            ch0Max = Math.Max(ch0Max, frame.Ch0!.Value);
            ch1Min = Math.Min(ch1Min, frame.Ch1!.Value);
            ch1Max = Math.Max(ch1Max, frame.Ch1!.Value);
            auxMin = Math.Min(auxMin, frame.Aux!.Value);
            auxMax = Math.Max(auxMax, frame.Aux!.Value);
            ch0Sum += frame.Ch0!.Value;
            ch1Sum += frame.Ch1!.Value;
            auxSum += frame.Aux!.Value;
            auxCounts[frame.Aux!.Value]++;
            Console.WriteLine($"{index:000} {timeMs:0000000} {FormatSample(incomFrame)} {frame.Ch0!.Value:00000} {frame.Ch1!.Value:00000} {frame.Aux!.Value:000}");
            #endregion
            index++;
            sampleBytes = 0;
        }

        sw.Stop();
        var elapsedSec = Math.Max(sw.Elapsed.TotalSeconds, 0.001);
        var hz = TargetFrameCount / elapsedSec;

        #region Footer
        Console.WriteLine("SUMMARY");
        Console.WriteLine($"RAW_BYTES={rawBytes}");
        Console.WriteLine($"SAMPLES={TargetFrameCount}");
        Console.WriteLine($"SKIPPED_BYTES={skippedBytes}");
        Console.WriteLine("TAIL_BYTES=0");
        Console.WriteLine($"HZ={hz.ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MIN={ch0Min}");
        Console.WriteLine($"CH0_AVG={(ch0Sum / (double)TargetFrameCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH0_MAX={ch0Max}");
        Console.WriteLine($"CH1_MIN={ch1Min}");
        Console.WriteLine($"CH1_AVG={(ch1Sum / (double)TargetFrameCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"CH1_MAX={ch1Max}");
        Console.WriteLine($"AUX_MIN={auxMin}");
        Console.WriteLine($"AUX_AVG={(auxSum / (double)TargetFrameCount).ToString("0.00", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"AUX_MAX={auxMax}");
        Console.WriteLine($"AUX_COUNTS={FormatAuxCounts(auxCounts)}");
        #endregion
    }

    // при приходе нового байта добавляю в массив, увеличиваю счетчик
    private static void AddByte(byte[] incomFrame, ref int frameBytes, byte value)
    {
        incomFrame[frameBytes] = value;
        frameBytes++;
    }

    private static void ShiftLeft(byte[] sample, ref int sampleBytes)
    {
        for (var index = 1; index < sampleBytes; index++)
            sample[index - 1] = sample[index];

        sampleBytes--;
    }


    private static byte ReadByte(SerialPort sp)
    {
        var value = sp.ReadByte();
        if (value < 0) throw new TimeoutException("COM read returned no data.");
        return (byte)value;
    }

    private static string FormatSample(byte[] data)
        => string.Join(" ", Array.ConvertAll(data, b => b.ToString("000", CultureInfo.InvariantCulture)));

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

    private static void PrintHeader(string portName, int sampleCount)
    {
        Console.WriteLine($"MODE={ModeName}");
        Console.WriteLine($"PORT={portName}");
        Console.WriteLine($"SET={SetByte}");
        Console.WriteLine($"REQ={ReqByte}");
        Console.WriteLine("REQUEST_MODE=STREAM_AFTER_SINGLE_REQ");
        Console.WriteLine($"SAMPLES_TARGET={sampleCount}");
        Console.WriteLine($"SAMPLE_SIZE={IncommingMessageSize}");
        Console.WriteLine("SYNC_RULE=AUX=(B0+B1+B2+B3)&0xFF");
        Console.WriteLine("FORMAT=SAMPLE5_UINT16_LE");
        Console.WriteLine("SAMPLE=CH0_LO CH0_HI CH1_LO CH1_HI AUX");
        Console.WriteLine($"{ "IDX",3} { "TIME_MS",7} { "B0",3} { "B1",3} { "B2",3} { "B3",3} { "B4",3} { "CH0",5} { "CH1",5} { "AUX",3}");
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
        Console.Error.WriteLine("USAGE=DynamicDumpDec COM1 40");
    }

    private static void WaitBeforeExit()
    {
        Console.WriteLine("DONE=PRESS_ENTER_TO_EXIT");
        Console.ReadLine();
    }
}
