using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class IssueLicenseDto
    {
        public int LocalDrivingLicenseApplicationId {  get; set; }
        public int CreatedByUserId { get; set; }
        public string Note {  get; set; }
    }
}
