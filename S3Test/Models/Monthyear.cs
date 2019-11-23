using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Monthyear
    {
        public Monthyear()
        {
            Monthmain = new HashSet<Monthmain>();
        }

        public int MonthyearId { get; set; }
        public int MonthId { get; set; }
        public short RtnPeriod { get; set; }
        public bool Quarterly { get; set; }

        public virtual ICollection<Monthmain> Monthmain { get; set; }
    }
}
