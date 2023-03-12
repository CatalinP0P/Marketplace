using Marketplace.Models;

namespace Marketplace.ViewModels
{
    public class MessagesViewModel
    {
        public List<Chatmessage> Chatmessages { get; set; }
        public string SenderState { get; set; } // Buyer / Seller;
        public int anuntId { get; set; }
    }
}