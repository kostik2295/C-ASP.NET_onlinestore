using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyStoreWEB.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        [ForeignKey("ProducerId")]
        public ICollection<Product> Products { get; set; }
    }
}
