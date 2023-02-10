using Marketplace.Interfaces;
using Marketplace.Data;
using Marketplace.ViewModels;
using Marketplace.Models;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.ViewModels
{
    public class FormViewModel
    {  
        public Anunt? Anunt { get; set; }
        public List<string>? Counties { get; set; }
        public List<string>? Cities { get; set; }
        public List<Categorie>? Categories { get; set; }


    }
}