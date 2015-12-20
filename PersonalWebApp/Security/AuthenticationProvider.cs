using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using log4net;
using System.Security.Claims;
using Microsoft.Practices.Unity;
using Spring.Web.Providers;
using PersonalWebApp.Models;

namespace PersonalWebApp.Security
{
    public abstract class AuthenticationProvider : IAuthenticationProvider
    {
        [Dependency]
        public IMembershipService MemberService { get; set; }

        [Dependency]
        public IRoleProvider RoleProvider { get; set; }
        public abstract void Configure(IAppBuilder app);
        public abstract void SignIn(string userid, string returnUrl);
        public abstract void SignOut();

        public IEnumerable<Claim> GetClaimsForUser(string userid)
        {
            List<Claim> list_claims = null;
            UserModel user = MemberService.GetUser(userid);
            if (user != null)
            {
                list_claims = new List<Claim>();
                list_claims.Add(new Claim(ClaimTypes.Sid, user.Id));
                list_claims.Add(new Claim(ClaimTypes.Name, user.Name));
                list_claims.Add(new Claim(ClaimTypes.Email, user.Email));
                //list_claims.AddRange(RoleProvider.GetRolesForUser(userid).Select(x => new Claim(ClaimTypes.Role, x)));
            }
            return list_claims;
        }
    }
}