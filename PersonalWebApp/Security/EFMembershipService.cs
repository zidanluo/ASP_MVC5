using PersonalWebApp.Data;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebApp.Security
{
    public class EFMembershipService : IMembershipService
    {
        public ValidateResult ValidateUser(string userid, string passwd)
        {
            using (WebAppDbContext db = new Data.WebAppDbContext())
            {

                if (db.Users.Any(x => x.UserId == userid && x.Passwd == passwd))
                    return ValidateResult.Success;
                else
                    return ValidateResult.Failure;
            }
        }


        public UserModel GetUser(string userid)
        {
            using (WebAppDbContext db = new Data.WebAppDbContext())
            {
                var user = db.Users.FirstOrDefault(x=>x.UserId == userid);
                if (user == null)
                    return null;
                return new UserModel
                {
                    Id = userid,
                    Name = user.UserName,
                    Email = user.Email,
                };          
            }
        }
    }
}