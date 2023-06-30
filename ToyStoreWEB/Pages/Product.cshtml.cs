using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class ProductModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public Product Product { get; set; }
        private static int Id { get; set; }
        public static int Count { get; set; }
        public bool Status { get; set; }
        public static string CommentStatus { get; set; } = "hidden"; 
        public void Load()
        {
            db.Products.Include(x => x.Category).Include(x => x.Producer).Load();
            Product = db.Products.FirstOrDefault(x => x.Id == Id);
            foreach (var item in UserInfo.Bucket)
                if (item.Product == Product)
                    Status = true;
        }
        public void OnGet(int id)
        {
            Id = id;
            Load();
            Count = 0;
        }
        public void OnPostBucket(int count)
        {
            Count = count;
            Load();
			UserInfo.Bucket.Add(new BucketProduct { Product = Product, Count = count });
			Load();
        }
    }
}
