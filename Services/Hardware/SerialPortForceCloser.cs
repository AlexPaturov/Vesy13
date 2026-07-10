using System.IO.Ports;
using System.Runtime.InteropServices;

namespace Vesy13.Services.Hardware;

/// <summary>
/// Обходит зависание <see cref="SerialPort.Close"/>, когда устройство на другом конце
/// линии не отвечает: внутри SerialStream.Dispose() штатное закрытие ждёт (без таймаута)
/// завершения фонового overlapped-цикла WaitCommEvent. Если он не укладывается в разумное
/// время, обрываем pending I/O через CancelIoEx на сыром хендле — это будит зависший
/// GetOverlappedResult, дальше SerialStream доделывает закрытие сам, штатным путём.
/// </summary>
internal static class SerialPortForceCloser
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

    /// <summary>
    /// Закрывает порт. Возвращает true, если закрылось штатно в пределах <paramref name="timeoutMs"/>,
    /// false — если пришлось форсировать обрыв pending I/O (вызывающий код должен считать
    /// SerialPort дальше непригодным для повторного использования).
    /// </summary>
    public static bool CloseWithTimeout(SerialPort port, int timeoutMs = 500, int forceGraceMs = 2000)
    {
        if (!port.IsOpen) return true;

        SafeHandle? handle = TryGetHandle(port);

        var closeTask = Task.Run(port.Close);
        if (closeTask.Wait(timeoutMs)) return true;

        if (handle is not null)
            TryCancelPendingIo(handle);

        closeTask.Wait(forceGraceMs);
        return false;
    }

    private static SafeHandle? TryGetHandle(SerialPort port)
    {
        try
        {
            var baseStream = port.BaseStream;
            var field = baseStream.GetType().GetField("_handle",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return field?.GetValue(baseStream) as SafeHandle;
        }
        catch
        {
            return null;
        }
    }

    private static void TryCancelPendingIo(SafeHandle handle)
    {
        bool refAdded = false;
        try
        {
            handle.DangerousAddRef(ref refAdded);
            if (refAdded)
                CancelIoEx(handle.DangerousGetHandle(), IntPtr.Zero);
        }
        catch
        {
            // best-effort: если не получилось - просто ждём forceGraceMs штатного завершения
        }
        finally
        {
            if (refAdded)
                handle.DangerousRelease();
        }
    }
}
