namespace parcial.Models;


public class TransactionBTC
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
}