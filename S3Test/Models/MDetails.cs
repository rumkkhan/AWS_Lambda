using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.Models
{
    public class MDetails
    {


        public int AnxId { get; set; }
        public int TranId { get; set; }
        public int? MonthId { get; set; }
        public short? SectionId { get; set; }
        public string OrgGstin { get; set; }
        public string RevisedGstin { get; set; }
        public int? PartyId { get; set; }
        public string OrgTradeName { get; set; }
        public string RevisedTradeName { get; set; }
        public short? OrgDocType { get; set; }
        public short? RevisedDocType { get; set; }
        public string OrgDocNum { get; set; }
        public string RevisedDocNum { get; set; }
        public DateTime? OrgDocDate { get; set; }
        public DateTime? RevisedDocDate { get; set; }
        public decimal? DocValue { get; set; }
        public short? Pos { get; set; }
        public short? SupplyType { get; set; }
        public short? ExportType { get; set; }
        public string ShippingNum { get; set; }
        public string ShippingDate { get; set; }
        public string PortCode { get; set; }
        public decimal? DiffRate { get; set; }
        public int? BranchId { get; set; }
        public short? FinYear { get; set; }
        public bool? IsAmended { get; set; }
        public string Flag { get; set; }
        public string ActionFlag { get; set; }
        public bool? RefndElg { get; set; }
        public string Checksum { get; set; }
        public string RelChecksum { get; set; }
        public bool? ClaimRfnd { get; set; }
        public short? CompareType { get; set; }
        public string ItcEntitle { get; set; }
        public bool? IgstAct { get; set; }
        public bool? IsRefund { get; set; }
        public string TableRef { get; set; }
        public string ItcEnteredBlc { get; set; }

        public virtual Branchmaster Branch { get; set; }
        public virtual Partymaster Party { get; set; }
        public virtual Monthmain Tran { get; set; }
        public virtual ICollection<Trananxdet> Trananxdet { get; set; }
    }

   
}
