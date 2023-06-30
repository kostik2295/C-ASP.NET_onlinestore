//using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class RegistrModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public IActionResult OnPost(string login, string password, string repeatPassword)
        {
            try
            {
                db.Users.Load();
                User user = db.Users.FirstOrDefault(x => x.Login == login);
                if (user == null)
                {
                    if (password == repeatPassword)
                    {
                        db.Users.Add(new User { Login = login, Password = password });
                        db.SaveChanges();
                        User current = db.Users.FirstOrDefault(x => x.Login == login);
                        UserInfo.UserId = current.Id;
                        UserInfo.Status = UserStatus.Account;
                        return RedirectToPage("/Index");
                    }
                }
                return RedirectToPage("/Registr");
            }
            catch (Exception)
            {
                throw new Exception("Ошибка регистрации");
            }
        }
    }
}
