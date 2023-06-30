//using Google.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class IndexModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Producer> Producers { get; set; } = new List<Producer>();
        public void Load()
        {
            db.Products.Include(x => x.Category).Include(x => x.Producer).Load();
            db.Categories.Load();
            db.Producers.Load();
            Products = db.Products.ToList();
            Categories = db.Categories.ToList();
            Producers = db.Producers.ToList();
        }
        public void OnGet(bool exit = false)
        {
            Load();
            if (exit)
            {
                UserInfo.Status = UserStatus.Login;
            }
            if (UserInfo.Status is UserStatus.AdminExit)
            {
                UserInfo.Status = UserStatus.Admin;
            }
            if (UserInfo.Status is UserStatus.AccountExit)
            {
                UserInfo.Status = UserStatus.Account;
            }
        }

        public void OnPostFilter(string category, string producer)
        {
            Load();
            if (category != null)
                Products = Products.Where(x => x.Category.Id == Convert.ToInt32(category)).ToList();
            if (producer != null)
                Products = Products.Where(x => x.Producer.Id == Convert.ToInt32(producer)).ToList();
        }

        public void OnPostReset()
        {
            Load();
        }

    }
}