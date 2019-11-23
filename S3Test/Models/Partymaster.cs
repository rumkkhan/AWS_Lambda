using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Partymaster
    {
        public Partymaster()
        {
            Trananx = new HashSet<Trananx>();
        }

        public int PartyId { get; set; }
        public int ClientId { get; set; }
        public int EntityId { get; set; }
        public string Address { get; set; }
        public string PartyName { get; set; }
        public string Gstin { get; set; }
        public string Place { get; set; }
        public short? PartyType { get; set; }
        public bool? IsSez { get; set; }
        public short? StateCode { get; set; }
        public short? SupplyType { get; set; }
        public string Pan { get; set; }
        public string Tan { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Trananx> Trananx { get; set; }
    }
}
