namespace Marketplace.Models
{
    public class Chatmessage
    {
        public int Id { get; set; }
        public int ChatroomId { get; set; }
        public string Sender { get; set; } // Buyer or Seller
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}