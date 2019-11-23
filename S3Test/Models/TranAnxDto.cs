using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.Models
{
    public class TranAnxDto
    {
  

        public string OrgGstin { get; set; } //origional GSTN
        public string PartyName { get; set; }
        public string Typee { get; set; }
        public string InNumber { get; set; }
        public string  Date { get; set; }

        public string DocValue { get; set; }

        public string TaxableValue { get; set; }
      
        public string Rate { get; set; }
        public string  Igst { get; set; }
        public string Cgst { get; set; }
        public string Sgst { get; set; }
        public string Cess { get; set; }
        public string Pos { get; set; }
        public string  HSN { get; set; }
        public string  Diff { get; set; }
        public string Supply { get; set; }
        public virtual ICollection<Trananxdet> Trananxdet { get; set; }

    }
}
