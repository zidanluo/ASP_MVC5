using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebApp.Security
{
    public interface IMembershipService
    {
        ValidateResult ValidateUser(string username, string passwd);

        UserModel GetUser(string username);
    }
}