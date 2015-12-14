using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWebApp.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        [StringLength(15)]
        public string UserId { get; set; }

        [StringLength(60)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string Passwd { get; set; }

        [StringLength(30)]
        public string Email { get; set; }
    }
}