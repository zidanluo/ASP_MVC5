using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PersonalWebApp.Security
{
    public interface IAuthenticationProvider
    {
        void Configure(IAppBuilder app);
        void SignIn(string username, string returnUrl);
        void SignOut();

        IEnumerable<Claim> GetClaimsForUser(string username);
    }
}
