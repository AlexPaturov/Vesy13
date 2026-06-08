namespace Vesy13.Models;

public class CalibPoint
{
    public int     Id       { get; set; }
    public int     Channel  { get; set; }  // 0 = CH0, 1 = CH1
    public int     AdcCode  { get; set; }
    public decimal Mass     { get; set; }
    public bool    IsActive { get; set; } = true;
}
