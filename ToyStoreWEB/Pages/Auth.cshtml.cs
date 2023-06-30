using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class AuthModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public IActionResult OnPostLogin(string login, string password)
        {
            try
            {
                db.Users.Load();
                User user = db.Users.FirstOrDefault(x => x.Login == login);
                if (user != null)
                {
                    if (user.Login == "admin")
                    {
                        UserInfo.UserId = user.Id;
                        UserInfo.Status = UserStatus.Admin;
                        return RedirectToPage("/Index");
                    }
                    if (user.Password == password)
                    {
                        UserInfo.UserId = user.Id;
                        UserInfo.Status = UserStatus.Account;
                        return RedirectToPage("/Index");
                    }
                }
                return RedirectToPage("/Auth");
            }
            catch (Exception)
            {
                throw new Exception("Ошибка авторизации");
            }
		}
    }
}
