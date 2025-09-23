namespace AbySalto.Junior.Models.DTOs
{
    public class CreateOrderDTO
    {
        public string BuyerName { get; set; }
        public string Addres { get; set; }
        public string ContactNumber { get; set; }
        public string? Remark { get; set; }
        public int PaymentTypeId { get; set; }
        public int CurrencyId { get; set; }
    }
}
