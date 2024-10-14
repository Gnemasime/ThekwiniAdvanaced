using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThekwiniAdvanaced.Models
{
    public class EstatePenalty
    {
        [Key]
      // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PenaltyId { get; set; }
        public int OwnerId { get; set; }
        public int AreaCode { get; set; }
        public int ViolationCode { get; set; }
        public decimal TotalPenaltyCost { get; set; }

        //Foreign
        public virtual Owner Owner { get; set; }
        public virtual Area Area { get; set; }
        public virtual Violation Violation {get;set;}

        AppDBContext db = new AppDBContext();
        public decimal PullVCost()
        {
           
            var vcost = (from c in db.violations
                         where c.ViolationCode == ViolationCode
                         select c.ViolationCost).FirstOrDefault();
            return vcost;
        }
        public decimal PullAreaRAte()
        {
            
            var vrate = (from c in db.areas
                         where c.AreaCode == AreaCode
                         select c.AreaRate).FirstOrDefault();
            return vrate;
        }

        public decimal CalcAreaPenalty()
        {
            return PullVCost() * (PullAreaRAte()/100.0m);
        }

        //1.5
        public  decimal CalcPointstoDeduct()
        {
            return Math.Floor(PullVCost() * 0.01m);
        }
        //1.6
        public void UpdatePoints()
        {
            AppDBContext db = new AppDBContext();
            Owner owner = (from b in db.owners
                           where b.OwnerId == OwnerId
                           select b).FirstOrDefault();

            owner.OwnerPoints -= Convert.ToInt32(CalcPointstoDeduct()); //owner.OwnerPoints = owner.OwnerPoints - Convert.ToInt32(CalcPointsToDeduct())

            if(owner.OwnerPoints <= 0)
            {
                owner.OwnerPoints = 0;
                owner.Status = "Invalid";
            }
            db.SaveChanges();
        }

        //1.6
        public bool CheckLicense()
        {
            AppDBContext db = new AppDBContext();
            Owner owner = (from d in db.owners
                           where d.OwnerId == OwnerId
                           select d).FirstOrDefault();

            if(owner.Status=="Valid")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}