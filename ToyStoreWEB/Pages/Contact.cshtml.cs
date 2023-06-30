using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToyStoreWEB.Pages
{
    public class ContactModel : PageModel
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
    }
}
