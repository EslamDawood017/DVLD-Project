using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class NewDetainDto
    {
        public int DetainId { get; set; }
        public int LicenseID { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
    }
}
