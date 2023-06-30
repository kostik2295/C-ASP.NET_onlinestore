using ToyStoreWEB.Models;

namespace ToyStoreWEB
{
    public enum UserStatus{
        Login,
        Admin,
        Account,
        AdminExit,
        AccountExit
    }
    public class UserInfo
    {
        public static int UserId { get; set; }
        public static UserStatus Status { get; set; } = UserStatus.Admin;
        public static List<BucketProduct> Bucket { get; set; } = new List<BucketProduct>();
    }
}
