using Microsoft.AspNetCore.Mvc;

using Marketplace.Data;
using Marketplace.Models;
using System.Security.Claims;

namespace Marketplace.Controllers.Api
{
    [ApiController]
    [Route("api/{controller}")]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAnuntChatRooms/{anuntId}")]
        public List<Chatroom> GetAnuntChatRooms(int anuntId) 
        {
            List<Chatroom> chatRooms = _context.Chatrooms.Where(m=>m.AnuntId == anuntId).ToList();
            chatRooms.Reverse();
            return chatRooms;
        }

        [HttpGet]
        [Route("exists/{anuntId}")]
        public bool existsChatRoom( int anuntId )
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRoomInDb = _context.Chatrooms.SingleOrDefault(m=>m.AnuntId == anuntId && m.BuyerId == userId );
            if ( chatRoomInDb == null )
                return false;
            
            return true;
        }

        [HttpPost]
        [Route("createChat/{anuntId}")]
        public int createChatRoom(int anuntId)
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            if ( userId == "notfound" ) return -1;

            var anunt = _context.Anunturi.Single(m=>m.Id == anuntId);

            var chatRoom = new Chatroom()
            {
                AnuntId = anuntId,
                BuyerId = userId,
                SellerId = anunt.AccountId,
            };

            _context.Chatrooms.Add(chatRoom);
            _context.SaveChanges();
            
            var chatRoomId = _context.Chatrooms.Single( m=>m.AnuntId == chatRoom.AnuntId && m.BuyerId == chatRoom.BuyerId && m.SellerId == chatRoom.SellerId ).Id; 

            return chatRoomId;
        }

        [HttpGet]
        [Route("GetChatRoomMessages/{chatId}")]
        public List<Chatmessage> GetChatRoomMessages(int chatId)
        {
            var chatMessages = _context.Chatmessages.Where(m=>m.ChatroomId == chatId).ToList();
            chatMessages.Reverse();
            return chatMessages;
        }

        [HttpGet]
        [Route("GetLast/{chatId}")]
        public Chatmessage GetLastMessage(int chatId)
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRoom = _context.Chatrooms.SingleOrDefault( m => m.Id == chatId && ( m.BuyerId == userId || m.SellerId == userId ));
            
            if ( chatRoom== null ) return new Chatmessage();

            var chatMessages = _context.Chatmessages.Where(m=>m.ChatroomId == chatId ).ToList();
            
            return chatMessages.Last();
        }

        [HttpPost]
        [Route("sendMessage/{chatRoomId}")]
        public string SendMessage(int chatRoomId, string message)
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRoom = _context.Chatrooms.SingleOrDefault(m=>m.Id == chatRoomId);
            if ( chatRoom == null )
                return "failed";
            
            var chatmessage = new Chatmessage();
            chatmessage.Date = DateTime.Now;
            chatmessage.Message = message;
            chatmessage.ChatroomId = chatRoom.Id;

            var anunt = _context.Anunturi.Single(m=>m.Id == chatRoom.AnuntId );
            if ( anunt.AccountId == userId ) 
            {
                chatmessage.Sender = "Seller";
            }
            else if ( chatRoom.BuyerId == userId )
            {
                chatmessage.Sender = "Buyer";
            }

            _context.Chatmessages.Add(chatmessage);
            _context.SaveChanges();

            return "message send";
        }
    }
}
