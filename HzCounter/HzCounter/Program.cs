using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

/// <summary>
/// HzCounter — только измерение частоты пакетов.
/// Настройки COM-порта совпадают с АЦП «СИМ А04».
/// Запуск: dotnet run -- COM4
/// </summary>

namespace HzCounter;

internal class Program
{
    const int PACKET_SIZE = 4;   // int32 little-endian
    const int MEASURE_SECONDS = 30;  // время замера
    const int DEFAULT_BAUDRATE = 4800;
    const string DEFAULT_PORT = "COM4";

    static void Main(string[] args)
    {
        string portName = args.Length >= 1 ? args[0] : DEFAULT_PORT;

        Console.WriteLine($"Порт: {portName} @ {DEFAULT_BAUDRATE} baud");
        Console.WriteLine($"Замер: {MEASURE_SECONDS} сек...");
        Console.WriteLine();

        long bytesTotal = 0;
        int fullPackets = 0;  // пакеты кратные 4 байтам
        int remainder = 0;  // остаток байт (не кратно 4)

        using var sp = new SerialPort
        {
            PortName = portName,
            BaudRate = DEFAULT_BAUDRATE,
            DataBits = 8,
            Parity = Parity.Even,
            StopBits = StopBits.One,
            ReadTimeout = 500,
            WriteTimeout = 500
        };

        sp.Open();

        var sw = Stopwatch.StartNew();

        while (sw.Elapsed.TotalSeconds < MEASURE_SECONDS)
        {
            if (sp.BytesToRead == 0)
            {
                Thread.Sleep(1);
                continue;
            }

            // Читаем сколько есть — данные нигде не храним
            int available = sp.BytesToRead;
            bytesTotal += available;

            // Просто читаем в /dev/null буфер
            var throwaway = new byte[available];
            sp.Read(throwaway, 0, available);

            // Прогресс
            Console.Write($"\r  {sw.Elapsed.TotalSeconds:F0}/{MEASURE_SECONDS} сек   байт: {bytesTotal}   ");
        }

        sp.Close();

        double elapsedSec = sw.Elapsed.TotalSeconds;
        double bytesPerSec = bytesTotal / elapsedSec;
        double packetsPerSec = bytesPerSec / PACKET_SIZE;
        fullPackets = (int)(bytesTotal / PACKET_SIZE);
        remainder = (int)(bytesTotal % PACKET_SIZE);
        double lossPercent = remainder > 0
            ? (double)remainder / bytesTotal * 100.0
            : 0;

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("── Результат ─────────────────────────────────");
        Console.WriteLine($"  Время замера:        {elapsedSec:F2} сек");
        Console.WriteLine($"  Байт получено:       {bytesTotal}");
        Console.WriteLine($"  Байт/сек:            {bytesPerSec:F1}");
        Console.WriteLine($"  Частота (Hz):        {packetsPerSec:F2}");
        Console.WriteLine($"  Полных пакетов:      {fullPackets}");
        Console.WriteLine($"  Остаток байт:        {remainder}  ({lossPercent:F2}% потерь)");
        Console.WriteLine("──────────────────────────────────────────────");

    }
}
