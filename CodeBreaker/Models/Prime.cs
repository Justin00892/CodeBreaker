using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Code
{
    public class Prime
    {
        [Key]
        public int ID { get; set; }
        public int Size { get; set; }
        public string NumberString { get; set; }
    }
}
