using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Marketplace.Data;
using Marketplace.Models;
using Marketplace.ViewModels;

namespace Marketplace.Controllers
{
    public class ChatController : Controller
    {   
        private readonly ApplicationDbContext _context;
        
        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Buying", "Chat");
        }
        
        public IActionResult Buying()
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRooms = _context.Chatrooms.Where(m=>m.BuyerId == userId).ToList();
            chatRooms.Reverse();

            return View(chatRooms);
        }

        public IActionResult Selling()
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRooms = _context.Chatrooms.Where(m=>m.SellerId == userId).ToList();
            chatRooms.Reverse();

            return View(chatRooms);
        }

        public IActionResult Messages(int chatRoomId)
        {
            var userId = "notfound";
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                userId = claims.Value;
            }

            var chatRoom = _context.Chatrooms.FirstOrDefault(m=>m.Id == chatRoomId && ( m.SellerId == userId || m.BuyerId == userId ));
            if ( chatRoom == null )
                return RedirectToAction("Error404", "Error");
            
            var chatMessages = _context.Chatmessages.Where(m=>m.ChatroomId == chatRoom.Id).ToList();
            
            var viewModel = new MessagesViewModel();
            viewModel.Chatmessages = chatMessages;
            viewModel.anuntId = chatRoom.AnuntId;

            if ( chatRoom.SellerId == userId )
                viewModel.SenderState = "Seller";
            else if ( chatRoom.BuyerId == userId )
                viewModel.SenderState = "Buyer";


            return View(viewModel);
        }

    }
}