using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.Models.dto
{
    public class PartyMasterDto
    {

        public int ClientId { get; set; }
        public int EntityId { get; set; }
       public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string Gstin { get; set; }
        public short? StateCode { get; set; }
    }
}
