namespace AbySalto.Junior.Models.DTOs
{
    public class CreateOrderItemDTO
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ArticleId {  get; set; }
    }
}