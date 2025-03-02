using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class TestDto
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool? TestResult { get; set; } // Nullable, assuming test result may be null
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public int ApplicantPersonID { get; set; }
    }
}
