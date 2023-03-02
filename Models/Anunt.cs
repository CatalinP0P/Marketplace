using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class Anunt
    {
        [Required]
        public int Id { get; set; }

        public string? AccountId { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [MinLength(10, ErrorMessage = "Titlul trebuie sa contina minim 10 caractere")]
        [MaxLength(100, ErrorMessage = "Titlul trebuie sa contina maxim 100 caractere")]
        public string? Title { get; set; }
        
        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [MinLength(60, ErrorMessage = "Descrierea trebuie sa contina minim 60 caractere")]
        [MaxLength(2000, ErrorMessage = "Descrierea trebuie sa contina maxim 2000 caractere")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        public string? County { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        public string? City { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Mailul nu este valid")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Phone]
        public string? NumarTelefon { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        public string? Category { get; set; }
    }

}