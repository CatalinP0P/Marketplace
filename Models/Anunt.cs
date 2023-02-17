using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class Anunt
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public string? Title { get; set; }
        
        public byte[] Image { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string? County { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string? Mail { get; set; }

        [MaxLength(11)]
        [MinLength(10)]
        [Required]
        public string? NumarTelefon { get; set; }

        [Required]
        public string? Category { get; set; }
    }

}