using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.Models
{
    public class TranAnxDto
    {
  
        public int AnxId { get; set; }
        public int TranId { get; set; }
        public int? MonthId { get; set; }
        public short? SectionId { get; set; }

        public string OrgGstin { get; set; } //origional GSTN
        public string PartyName { get; set; }
        public string Typee { get; set; }
        public string InNumber { get; set; }
        public string  Date { get; set; }

        public decimal? DocValue { get; set; }

        public decimal? TaxableValue { get; set; }
      
        public decimal? Rate { get; set; }
        public decimal?  Igst { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }
        public decimal Cess { get; set; }
        public string Pos { get; set; }
        public string  HSN { get; set; }
        public string  Diff { get; set; }
        public string Supply { get; set; }
        public virtual Branchmaster Branch { get; set; }
        public virtual Partymaster Party { get; set; }
        public virtual Monthmain Tran { get; set; }
        public virtual ICollection<Trananxdet> Trananxdet { get; set; }

    }
}
