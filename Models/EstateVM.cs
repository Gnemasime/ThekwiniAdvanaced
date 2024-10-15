using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThekwiniAdvanaced.Models
{
    public class EstateVM
    {
        [Key]
        public int EstateVMId { get; set; }
        public string Ownername { get; set; }
        public int OwnerPoints { get; set; }
        public string Status { get; set; }
        public string ViolationName { get; set; }
        public string AreaName { get; set; }

        [DisplayName("Total Penalty Cost")]
        public decimal T_Cost { get; set; }
    }
}