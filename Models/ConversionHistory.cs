namespace parcial.Models;



public class ConversionHistory
{
    public int Id { get; set; }
    public decimal BtcAmount { get; set; }
    public decimal UsdAmount { get; set; }
    public DateTime ConversionDate { get; set; }
}