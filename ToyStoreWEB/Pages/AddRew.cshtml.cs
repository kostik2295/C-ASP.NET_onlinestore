using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class AddRewModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public User User { get; set; } 
        public void OnGet()
        {
            db.Users.Load();
            db.Orders.Load();
            User = db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId);
        }
        public IActionResult OnPost(string adress, string phone, string comment)
        {
            db.Users.Load();
            db.Orders.Load();
            User = db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId);
            db.Orders.Add(new Order { User = User, Comment = comment, Date = DateTime.Now });
            db.SaveChanges();
            db.Orders.Load();
            Order order = db.Orders.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == User.Id);
            foreach (var product in UserInfo.Bucket)
            {
                Product product2 = db.Products.FirstOrDefault(x => x.Id == product.Product.Id);
                db.ProductsInOrder.Add(new ProductInOrder { Order = order, Product = product2, Count = product.Count });
			}
            db.SaveChanges();
            UserInfo.Bucket = new List<BucketProduct>();
            return RedirectToPage("/Index");
        }
    }
}
