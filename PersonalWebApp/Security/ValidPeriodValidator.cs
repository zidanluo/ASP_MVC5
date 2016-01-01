using log4net;
using PersonalWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PersonalWebApp.Security
{
    public class ValidPeriodValidator : ISecurityValidator
    {
        private ILog log = LogManager.GetLogger(typeof(ValidPeriodValidator));
        public bool Validate(string code)
        {
            try
            {
                using (WebAppDbContext db = new WebAppDbContext())
                {
                    DateTime? expdt = db.AccessCodes.Where(x => x.Code == code).Select(x => x.ExpireDt).FirstOrDefault();
                    if (expdt != null && expdt.HasValue && DateTime.Now.CompareTo(expdt.Value) < 0)
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
    }
}