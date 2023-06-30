using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToyStoreWEB.Models;

namespace ToyStoreWEB.Pages
{
    public class BucketModel : PageModel
    {
        public void OnGet()
        {
            if (UserInfo.Status is UserStatus.AdminExit)
            {
                UserInfo.Status = UserStatus.Admin;
            }
            if (UserInfo.Status is UserStatus.AccountExit)
            {
                UserInfo.Status = UserStatus.Account;
            }
        }

        public void OnPostDelete(int id) 
        {
            UserInfo.Bucket.RemoveAt(id);
        }
    }
}
