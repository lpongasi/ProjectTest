using Project.Entities.Identity;
using Project.Entities.User;
using Project.Models.Account;

namespace WebApp.Extensions
{
    public static class AppUserExt
    {
        public static void Register(this ApplicationUser user,RegisterViewModel rvm)
        {
            user.UserName = rvm.Email;
            user.Email = rvm.Email;
            user.UserId = rvm.UserId;
            user.FirstName = rvm.FirstName;
            user.LastName = rvm.LastName;
            user.MiddleName = rvm.MiddleName;
            user.CompanyName = rvm.CompanyName;
            user.CompanyContact = rvm.CompanyContact;
            user.CompanyAddress = rvm.CompanyAddress;
            user.Discount = rvm.Discount / (decimal)100;
            user.Tax = rvm.Tax / (decimal)100;
            user.OtherRemarks = rvm.OtherRemarks;
            user.OtherFees = rvm.OtherFees;
        }

        public static void Update(this ApplicationUser user, UpdateUserViewModel uvm)
        {
            user.UserId = uvm.UserId;
            user.FirstName = uvm.FirstName;
            user.LastName = uvm.LastName;
            user.MiddleName = uvm.MiddleName;
            user.CompanyName = uvm.CompanyName;
            user.CompanyContact = uvm.CompanyContact;
            user.CompanyAddress = uvm.CompanyAddress;
            user.Discount = uvm.Discount / (decimal)100;
            user.Tax = uvm.Tax / (decimal)100;
            user.OtherRemarks = uvm.OtherRemarks;
            user.OtherFees = uvm.OtherFees;
        }
        public static void UserUpdate(this ApplicationUser user, UpdateUserViewModel uvm)
        {
            user.UserId = uvm.UserId;
            user.FirstName = uvm.FirstName;
            user.LastName = uvm.LastName;
            user.MiddleName = uvm.MiddleName;
            user.CompanyName = uvm.CompanyName;
            user.CompanyContact = uvm.CompanyContact;
            user.CompanyAddress = uvm.CompanyAddress;
        }
        public static void Register(this UserInfo userInfo, ApplicationUser user)
        {
            userInfo.Id = user.Id;
            userInfo.Email = user.Email;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.MiddleName = user.MiddleName;
            userInfo.CompanyName = user.CompanyName;
            userInfo.CompanyAddress = user.CompanyAddress;
            userInfo.CompanyContact = user.CompanyContact;
        }
        public static void Update(this UserInfo userInfo, UpdateUserViewModel uvm)
        {
            userInfo.FirstName = uvm.FirstName;
            userInfo.LastName = uvm.LastName;
            userInfo.MiddleName = uvm.MiddleName;
            userInfo.CompanyName = uvm.CompanyName;
            userInfo.CompanyContact = uvm.CompanyContact;
            userInfo.CompanyAddress = uvm.CompanyAddress;
        }
    }
}
