namespace AbySalto.Junior.Models.DTOs
{
    public class AddItemOnOrderRequest
    {
        public int ArticleId {  get; set; }
        public int OrderId {  get; set; }
        public int Quantity {  get; set; }
    }
}