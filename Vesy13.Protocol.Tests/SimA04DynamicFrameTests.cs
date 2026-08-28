using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vesy13.Protocol.Tests;

[TestClass]
public class SimA04DynamicFrameTests
{
    [TestMethod]
    public void Parse_NullInput_ReturnsNullInputFrame()
    {
        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(null);

        Assert.AreEqual(FrameState.NullInput, frame.State);
        Assert.IsFalse(frame.IsValid);
        Assert.AreEqual(0, frame.ReceivedByteCount);

        Assert.IsNull(frame.B0);
        Assert.IsNull(frame.B1);
        Assert.IsNull(frame.B2);
        Assert.IsNull(frame.B3);
        Assert.IsNull(frame.Aux);
        Assert.IsNull(frame.Ch0);
        Assert.IsNull(frame.Ch1);
    }

    /// <summary>
    /// Тест пустого массива
    /// </summary>
    [TestMethod]
    public void Parse_EmptyInput_ReturnsEmptyFrame()
    {
        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(Array.Empty<byte>());

        Assert.AreEqual(FrameState.Empty, frame.State);
        Assert.IsFalse(frame.IsValid);
        Assert.AreEqual(0, frame.ReceivedByteCount);

        Assert.IsNull(frame.B0);
        Assert.IsNull(frame.B1);
        Assert.IsNull(frame.B2);
        Assert.IsNull(frame.B3);
        Assert.IsNull(frame.Aux);
        Assert.IsNull(frame.Ch0);
        Assert.IsNull(frame.Ch1);
    }

    /// <summary>
    /// одним параметризованным тестом покрытваю все неполные кадры длиной от 1 до 4 байтов
    /// </summary>
    /// <param name="length"></param>
    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    public void Parse_IncompleteInput_ReturnsIncompleteFrame(int length)
    {
        byte[] source = { 0x34, 0x12, 0x78, 0x56 };
        byte[] data = new byte[length];

        Array.Copy(source, data, length);

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.Incomplete, frame.State);
        Assert.IsFalse(frame.IsValid);
        Assert.AreEqual(length, frame.ReceivedByteCount);
        Assert.AreEqual((byte)0x34, frame.B0);
        Assert.AreEqual(length >= 2 ? (int?)0x1234 : null, frame.Ch0);
        Assert.AreEqual(length == 4 ? (int?)0x5678 : null, frame.Ch1);
    }

    /// <summary>
    /// Слишком длинный входной массив
    /// </summary>
    [TestMethod]
    public void Parse_InputLongerThanFrame_ReturnsInvalidLengthFrame()
    {
        byte[] data = { 1, 2, 3, 4, 5, 6 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.InvalidLength, frame.State);
        Assert.IsFalse(frame.IsValid);
        Assert.AreEqual(6, frame.ReceivedByteCount);

        Assert.IsNull(frame.B0);
        Assert.IsNull(frame.B1);
        Assert.IsNull(frame.B2);
        Assert.IsNull(frame.B3);
        Assert.IsNull(frame.Aux);
        Assert.IsNull(frame.Ch0);
        Assert.IsNull(frame.Ch1);
    }

    /// <summary>
    /// Полный кадр с неверной контрольной суммой.
    /// Проверяем, что данные всё равно разобраны и доступны для диагностики.
    /// Здесь правильный AUX был бы 0x14, а передан 0x15.
    /// </summary>
    [TestMethod]
    public void Parse_FrameWithInvalidChecksum_ReturnsInvalidChecksumFrame()
    {
        byte[] data = { 0x34, 0x12, 0x78, 0x56, 0x15 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.InvalidChecksum, frame.State);
        Assert.IsFalse(frame.IsValid);
        Assert.AreEqual(5, frame.ReceivedByteCount);

        Assert.AreEqual((byte)0x34, frame.B0);
        Assert.AreEqual((byte)0x12, frame.B1);
        Assert.AreEqual((byte)0x78, frame.B2);
        Assert.AreEqual((byte)0x56, frame.B3);
        Assert.AreEqual((byte)0x15, frame.Aux);

        Assert.AreEqual(0x1234, frame.Ch0);
        Assert.AreEqual(0x5678, frame.Ch1);
    }

    /// <summary>
    ///  Полный кадр с верной контрольной суммой.
    /// </summary>
    [TestMethod]
    public void Parse_FrameWithValidChecksum_ReturnsValidFrame()
    {
        byte[] data = { 0x34, 0x12, 0x78, 0x56, 0x14 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.Valid, frame.State);
        Assert.IsTrue(frame.IsValid);
        Assert.AreEqual(5, frame.ReceivedByteCount);

        Assert.AreEqual((byte)0x34, frame.B0);
        Assert.AreEqual((byte)0x12, frame.B1);
        Assert.AreEqual((byte)0x78, frame.B2);
        Assert.AreEqual((byte)0x56, frame.B3);
        Assert.AreEqual((byte)0x14, frame.Aux);

        Assert.AreEqual(0x1234, frame.Ch0);
        Assert.AreEqual(0x5678, frame.Ch1);
    }

    /// <summary>
    /// Переполнение контрольной суммы по модулю 256
    /// Расчёт: 255 + 255 + 1 + 2 = 513; 513 & 0xFF = 1
    /// </summary>
    [TestMethod]
    public void Parse_FrameWithOverflowingChecksum_ReturnsValidFrame()
    {
        byte[] data = { 0xFF, 0xFF, 0x01, 0x02, 0x01 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.Valid, frame.State);
        Assert.IsTrue(frame.IsValid);

        Assert.AreEqual(0xFFFF, frame.Ch0);
        Assert.AreEqual(0x0201, frame.Ch1);
        Assert.AreEqual((byte)0x01, frame.Aux);
    }

    /// <summary>
    /// проверить, что изменение любого из первых четырёх байтов делает ранее корректный кадр невалидным
    /// </summary>
    /// <param name="byteIndex"></param>
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void Parse_FrameWithCorruptedDataByte_ReturnsInvalidChecksumFrame(int byteIndex)
    {
        byte[] data = { 0x34, 0x12, 0x78, 0x56, 0x14 };

        data[byteIndex] = (byte)(data[byteIndex] + 1);

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.InvalidChecksum, frame.State);
        Assert.IsFalse(frame.IsValid);
    }

    /// <summary>
    /// Этот тест подтверждает, что объект хранит собственные значения байтов, а не ссылку на повторно используемый incomFrame.
    /// </summary>
    [TestMethod]
    public void Parse_InputArrayChangesAfterParsing_FrameKeepsOriginalData()
    {
        byte[] data = { 0x34, 0x12, 0x78, 0x56, 0x14 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        data[0] = 0;
        data[1] = 0;
        data[2] = 0;
        data[3] = 0;
        data[4] = 0;

        Assert.AreEqual(FrameState.Valid, frame.State);
        Assert.IsTrue(frame.IsValid);

        Assert.AreEqual((byte)0x34, frame.B0);
        Assert.AreEqual((byte)0x12, frame.B1);
        Assert.AreEqual((byte)0x78, frame.B2);
        Assert.AreEqual((byte)0x56, frame.B3);
        Assert.AreEqual((byte)0x14, frame.Aux);

        Assert.AreEqual(0x1234, frame.Ch0);
        Assert.AreEqual(0x5678, frame.Ch1);
    }

    /// <summary>
    /// Проверяю полностью нулевой кадр канала
    /// </summary>
    [TestMethod]
    public void Parse_ZeroFrame_ReturnsValidFrame()
    {
        byte[] data = { 0, 0, 0, 0, 0 };

        SimA04DynamicFrame frame = SimA04DynamicFrame.Parse(data);

        Assert.AreEqual(FrameState.Valid, frame.State);
        Assert.IsTrue(frame.IsValid);
        Assert.AreEqual(5, frame.ReceivedByteCount);

        Assert.AreEqual((byte)0, frame.B0);
        Assert.AreEqual((byte)0, frame.B1);
        Assert.AreEqual((byte)0, frame.B2);
        Assert.AreEqual((byte)0, frame.B3);
        Assert.AreEqual((byte)0, frame.Aux);

        Assert.AreEqual(0, frame.Ch0);
        Assert.AreEqual(0, frame.Ch1);
    }

    /// <summary>
    /// Проверить цикл «один буфер переиспользуется для следующего кадра».
    /// Первый кадр не должен измениться после разбора второго тем же массивом.
    /// </summary>
    [TestMethod]
    public void Parse_ReusedInputBuffer_ReturnsIndependentFrames()
    {
        byte[] buffer = { 0x34, 0x12, 0x78, 0x56, 0x14 };

        SimA04DynamicFrame firstFrame = SimA04DynamicFrame.Parse(buffer);

        buffer[0] = 0x01;
        buffer[1] = 0x00;
        buffer[2] = 0x02;
        buffer[3] = 0x00;
        buffer[4] = 0x03;

        SimA04DynamicFrame secondFrame = SimA04DynamicFrame.Parse(buffer);

        Assert.IsTrue(firstFrame.IsValid);
        Assert.AreEqual(0x1234, firstFrame.Ch0);
        Assert.AreEqual(0x5678, firstFrame.Ch1);
        Assert.AreEqual((byte)0x14, firstFrame.Aux);

        Assert.IsTrue(secondFrame.IsValid);
        Assert.AreEqual(1, secondFrame.Ch0);
        Assert.AreEqual(2, secondFrame.Ch1);
        Assert.AreEqual((byte)3, secondFrame.Aux);
    }



}

[TestClass]
public class CalibrationCalculatorTests
{
    [TestMethod]
    public void CalculateStatic_UsesFractionalCalibrationValueWithoutRounding()
    {
        var points = new[]
        {
            new CalibPoint { Channel = 0, AdcCode = 1400, Mass = 0, CalibrationValue = 0m },
            new CalibPoint { Channel = 0, AdcCode = 2400, Mass = 10, CalibrationValue = 655.350m },
        };

        var result = CalibrationCalculator.CalculateStatic(points, 1900, ActiveChannel.Main);

        Assert.IsNotNull(result);
        Assert.AreEqual(5d, result.Tonnes, 0.0000001d);
        Assert.AreEqual(655.350m, result.Point.CalibrationValue);
    }

    [TestMethod]
    public void CalculateStatic_ReturnsNegativeWeightBelowCalibrationTare()
    {
        var points = new[]
        {
            new CalibPoint { Channel = 0, AdcCode = 1400, Mass = 0, CalibrationValue = 0m },
            new CalibPoint { Channel = 0, AdcCode = 2400, Mass = 10, CalibrationValue = 655.350m },
        };

        var result = CalibrationCalculator.CalculateStatic(points, 1200, ActiveChannel.Main);

        Assert.IsNotNull(result);
        Assert.AreEqual(-2d, result.Tonnes, 0.0000001d);
    }

    [TestMethod]
    public void CalculateStatic_UsesLastApplicableScalePoint()
    {
        var points = new[]
        {
            new CalibPoint { Channel = 0, AdcCode = 1000, Mass = 0, CalibrationValue = 0m },
            new CalibPoint { Channel = 0, AdcCode = 2000, Mass = 10, CalibrationValue = 655.350m },
            new CalibPoint { Channel = 0, AdcCode = 3000, Mass = 20, CalibrationValue = 655.350m },
        };

        var result = CalibrationCalculator.CalculateStatic(points, 3500, ActiveChannel.Main);

        Assert.IsNotNull(result);
        Assert.AreEqual(25d, result.Tonnes, 0.0000001d);
        Assert.AreEqual(3000, result.Point.AdcCode);
    }
}
