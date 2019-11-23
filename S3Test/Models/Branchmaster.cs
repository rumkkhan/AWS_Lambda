using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Branchmaster
    {
        public Branchmaster()
        {
            Trananx = new HashSet<Trananx>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Trananx> Trananx { get; set; }
    }
}
