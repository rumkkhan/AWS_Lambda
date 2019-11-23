using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Monthmain
    {
        public Monthmain()
        {
            Trananx = new HashSet<Trananx>();
        }

        public int TranId { get; set; }
        public int MonthId { get; set; }
        public int ClientId { get; set; }
        public short? GstrNum { get; set; }
        public short? EntryType { get; set; }
        public short? Status { get; set; }
        public string AckNum { get; set; }
        public DateTime? AckDate { get; set; }
        public bool? Compared { get; set; }
        public bool? Submit { get; set; }
        public bool? Uploaded { get; set; }
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string UploadedBy { get; set; }

        public virtual Monthyear Month { get; set; }
        public virtual ICollection<Trananx> Trananx { get; set; }
    }
}
