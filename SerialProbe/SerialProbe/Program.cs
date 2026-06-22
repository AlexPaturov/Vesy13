/// <summary>
/// SerialProbe — замер частоты пакетов и захват сырого потока.
/// Настройки COM-порта совпадают с АЦП «СИМ А04».
/// Запуск: dotnet run -- COM4
/// </summary>

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace SerialProbe;

internal class Program
{
    // ── настройки ───────────────────────────────────────────────
    const int DEFAULT_BAUDRATE = 4800;
    const string DEFAULT_PORT = "COM4";
    const int MEASURE_SECONDS = 30;   // время замера частоты
    const int CAPTURE_SECONDS = 120;  // время захвата сырого потока
    const int PACKET_SIZE = 4;        // int32 little-endian
    // ────────────────────────────────────────────────────────────

    static void Main(string[] args)
    {
        string portName = args.Length >= 1 ? args[0] : DEFAULT_PORT;

        Console.WriteLine($"Порт: {portName} @ {DEFAULT_BAUDRATE} baud");
        Console.WriteLine();

        // ── 1. Замер частоты ────────────────────────────────────
        Console.WriteLine($"[1/2] Замер частоты — {MEASURE_SECONDS} сек...");
        var (bytesTotal, elapsedSec, corruptedPackets, totalPackets) =
            MeasureFrequency(portName, MEASURE_SECONDS);

        double bytesPerSec = bytesTotal / elapsedSec;
        double packetsPerSec = bytesPerSec / PACKET_SIZE;
        double corruptRatio = totalPackets > 0
            ? (double)corruptedPackets / totalPackets * 100.0
            : 0;

        Console.WriteLine();
        Console.WriteLine("── Результат замера ──────────────────────────");
        Console.WriteLine($"  Байт получено:       {bytesTotal}");
        Console.WriteLine($"  Время:               {elapsedSec:F2} сек");
        Console.WriteLine($"  Байт/сек:            {bytesPerSec:F1}");
        Console.WriteLine($"  Пакетов/сек (Hz):    {packetsPerSec:F2}");
        Console.WriteLine($"  Пакетов всего:       {totalPackets}");
        Console.WriteLine($"  Битых пакетов:       {corruptedPackets} ({corruptRatio:F1}%)");
        Console.WriteLine("──────────────────────────────────────────────");
        Console.WriteLine();

        // ── 2. Захват сырого потока ──────────────────────────────
        string captureFile = $"raw_{portName}_{DateTime.Now:yyyyMMdd_HHmmss}.bin";
        Console.WriteLine($"[2/2] Захват сырого потока — {CAPTURE_SECONDS} сек → {captureFile}");
        long captured = CaptureRaw(portName, CAPTURE_SECONDS, captureFile);

        Console.WriteLine();
        Console.WriteLine("── Результат захвата ─────────────────────────");
        Console.WriteLine($"  Файл:                {captureFile}");
        Console.WriteLine($"  Байт записано:       {captured}");
        Console.WriteLine($"  Пакетов (оценка):    {captured / PACKET_SIZE}");
        Console.WriteLine("──────────────────────────────────────────────");
    }

    // ── Замер частоты ────────────────────────────────────────────
    // Возвращает (bytesTotal, elapsedSec, corruptedPackets, totalPackets)
    // "Битый" пакет — скользящее окно 4 байта не даёт кратность,
    // считаем по простой эвристике: смотрим таймауты между байтами.
    static (long, double, int, int) MeasureFrequency(
        string port, int seconds)
    {
        long bytesTotal = 0;
        int corruptedPackets = 0;
        int totalPackets = 0;

        using var sp = OpenPort(port);
        sp.Open();

        // Буфер для скользящего окна
        var window = new byte[PACKET_SIZE];
        int windowPos = 0;
        int consecutive = 0; // подряд валидных пакетов (для ресинка)

        var sw = Stopwatch.StartNew();

        while (sw.Elapsed.TotalSeconds < seconds)
        {
            if (sp.BytesToRead == 0)
            {
                Thread.Sleep(1);
                continue;
            }

            int b = sp.ReadByte();
            if (b == -1) continue;

            bytesTotal++;
            window[windowPos] = (byte)b;
            windowPos++;

            if (windowPos == PACKET_SIZE)
            {
                totalPackets++;
                windowPos = 0;

                // Декодируем и проверяем на базовую вменяемость
                int value = BitConverter.ToInt32(window, 0);

                // Тут намеренно широкий диапазон — просто смотрим поток
                // Реальный [min,max] подберёшь после анализа файла
                if (value < -2_000_000 || value > 2_000_000)
                {
                    corruptedPackets++;
                    consecutive = 0;
                }
                else
                {
                    consecutive++;
                }
            }
        }

        sp.Close();
        return (bytesTotal, sw.Elapsed.TotalSeconds, corruptedPackets, totalPackets);
    }

    // ── Захват сырого потока в файл ──────────────────────────────
    static long CaptureRaw(string port, int seconds, string filename)
    {
        long captured = 0;
        var buffer = new byte[256];

        using var sp = OpenPort(port);
        using var file = new BinaryWriter(File.Open(filename, FileMode.Create));

        sp.Open();
        var sw = Stopwatch.StartNew();

        while (sw.Elapsed.TotalSeconds < seconds)
        {
            if (sp.BytesToRead == 0)
            {
                Thread.Sleep(1);
                continue;
            }

            int count = sp.Read(buffer, 0, Math.Min(buffer.Length, sp.BytesToRead));
            file.Write(buffer, 0, count);
            captured += count;

            // Прогресс каждые 5 сек
            if ((int)sw.Elapsed.TotalSeconds % 5 == 0)
            {
                Console.Write($"\r  Захвачено: {captured} байт, {sw.Elapsed.TotalSeconds:F0}/{seconds} сек   ");
            }
        }

        sp.Close();
        Console.WriteLine();
        return captured;
    }

    static SerialPort OpenPort(string port) => new SerialPort
    {
        PortName = port,
        BaudRate = DEFAULT_BAUDRATE,
        DataBits = 8,
        Parity = Parity.Even,
        StopBits = StopBits.One,
        ReadTimeout = 500,
        WriteTimeout = 500
    };
}
