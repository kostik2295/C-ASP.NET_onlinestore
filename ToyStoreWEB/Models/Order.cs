using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWEB.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("OrderId")]
        public ICollection<ProductInOrder> Products { get; set; }
    }
}
