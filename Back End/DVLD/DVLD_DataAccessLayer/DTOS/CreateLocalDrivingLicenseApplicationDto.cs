using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class CreateLocalDrivingLicenseApplicationDto
    {
        public int PersonId { get; set; }
        public int CreatedByUserId { get; set; }
        public int LisenceClassId { get; set; }
    }
}
