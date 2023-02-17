namespace Marketplace.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int IdAnunt { get; set; }
        public byte[] Bytes { get; set; }
    }
}