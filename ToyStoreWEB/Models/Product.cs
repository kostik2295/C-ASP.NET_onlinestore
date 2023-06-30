using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWEB.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [ForeignKey("ProductId")]
        public ICollection<ProductInOrder> Orders { get; set; }
    }
}
