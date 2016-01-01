using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWebApp.Data
{
    [Table("AccessCode")]
    public partial class AccessCode
    {
        [Key]
        [StringLength(12)]
        public string AccessId { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public DateTime? ExpireDt { get; set; }

        public DateTime? CreateDt { get; set; }

    }
}