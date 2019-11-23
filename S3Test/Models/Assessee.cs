using System;
using System.Collections.Generic;

namespace S3Test.Models
{
    public partial class Assessee
    {
        public int AssesseeId { get; set; }
        public int ClientId { get; set; }
        public int EntityId { get; set; }
        public string Address { get; set; }
        public string LegalName { get; set; }
        public string Place { get; set; }
        public string PinCode { get; set; }
        public short StateCode { get; set; }
        public short FinYear { get; set; }
        public string Gstin { get; set; }
        public DateTime RegDate { get; set; }
        public string Pan { get; set; }
        public string Tan { get; set; }
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Designation { get; set; }
        public short? Status { get; set; }
        public short? BusinessType { get; set; }
        public string UserPassword { get; set; }
        public bool? IsSez { get; set; }
        public string AltMobile { get; set; }
        public string TradeName { get; set; }
    }
}
