namespace Marketplace.Models
{
    public class Chatroom
    {
        public int Id { get; set; }
        public int AnuntId { get; set; }
        public string BuyerId { get; set; }
        public string SellerId { get; set; }
    }
}