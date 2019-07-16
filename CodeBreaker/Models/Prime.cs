using System.ComponentModel.DataAnnotations;

namespace CodeBreaker.Models
{
    public class Prime
    {
        [Key]
        public int ID { get; set; }
        public int Size { get; set; }
        public string NumberString { get; set; }
    }
}
