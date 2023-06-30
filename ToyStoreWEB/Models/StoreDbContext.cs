using System.Data.Entity;

namespace ToyStoreWEB.Models
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext() : base("Data Source=.\\SQLEXPRESS;Initial Catalog=Store;Integrated Security=True") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductInOrder> ProductsInOrder { get; set; }
    }
}
