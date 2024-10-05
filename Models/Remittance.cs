



namespace parcial.Models
{
    public class Remittance
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = "";
        public string RecipientName { get; set; } = "";
        public string OriginCountry { get; set; } = "";
        public string DestinationCountry { get; set; } = "";
        public decimal AmountSent { get; set; } = 0;
        public string Currency { get; set; } = "USD";
        public decimal ExchangeRate { get; set; } = 0;
        public decimal FinalAmount { get; set; } = 0;
        public string Status { get; set; } = "pending";
        public DateTime TransactionDate { get; set; }
    }

}