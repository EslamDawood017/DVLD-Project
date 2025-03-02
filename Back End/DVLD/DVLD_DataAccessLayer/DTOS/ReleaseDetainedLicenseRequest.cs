using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class ReleaseDetainedLicenseRequest
    {
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
    }
}
