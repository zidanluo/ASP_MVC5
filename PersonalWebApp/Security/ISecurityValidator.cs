using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWebApp.Security
{
    public interface ISecurityValidator
    {
        bool Validate(string code);
    }
}
