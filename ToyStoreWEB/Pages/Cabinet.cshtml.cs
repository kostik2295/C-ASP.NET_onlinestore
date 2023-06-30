using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class CabinetModel : PageModel
    {
        public StoreDbContext db = new StoreDbContext();
        public User User { get; set; }
        public static string StatusChange { get; set; } = "hidden";
        public List<Order> Orders { get; set; }
        public void Load()
        {
            db.Users.Load();
            db.Orders.Load();
            User = db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId);
            Orders = db.Orders.Where(x => x.UserId == UserInfo.UserId).Include(x => x.Products).ToList();
            Console.WriteLine();
        }
        public void OnGet()
        {
            Load();
            UserInfo.Status = UserStatus.AccountExit;
        }

        public void OnPostOpenChange()
        {
            Load();
            StatusChange = "";
        }
        public void OnPostChange(string adres, string phone)
        {
            Load();
            db.Users.Load();
            db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId).Adress = adres;
            db.Users.FirstOrDefault(x => x.Id == UserInfo.UserId).Phone = phone;
            db.SaveChanges();
            StatusChange = "hidden";
        }

        public IActionResult OnPostOpenBucket()
        {
            return RedirectToPage("/Bucket");
        }
    }
}
