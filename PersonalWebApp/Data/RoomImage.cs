using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalWebApp.Data
{
    [Table("RoomImage")]
    public partial class RoomImage
    {
        [Key]
        [Column(Order=1)]
        [StringLength(15)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RoomId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CaptureTime { get; set; }

        [StringLength(40)]
        public string FileName { get; set; }

        public DateTime CreateDt { get; set; }

    }
}