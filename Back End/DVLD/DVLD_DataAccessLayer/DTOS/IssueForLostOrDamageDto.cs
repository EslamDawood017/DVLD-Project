using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class IssueForLostOrDamageDto
    {
        public int LicenseId { get; set; }
        public int CreatedByUserId { get; set; }
        public int ReasonId { get; set; }
        public string? Note { get; set; }
    }
}
