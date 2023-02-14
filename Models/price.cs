using System.ComponentModel.DataAnnotations;

namespace pumafuelbar.Models
{
    public class price
    {
        [Key]
        public int id { get; set; }
        public double rate { get; set; }
    }
}
