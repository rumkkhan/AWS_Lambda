using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Trananxdet
    {
        public int DetId { get; set; }
        public int AnxId { get; set; }
        public string Hsnsac { get; set; }
        public decimal? Taxable { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Cess { get; set; }
        public short? Eligibility { get; set; }

        public virtual Trananx Anx { get; set; }
    }
}
