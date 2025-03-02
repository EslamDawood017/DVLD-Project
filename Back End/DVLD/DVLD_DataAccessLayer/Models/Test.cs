using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Models
{
    public class Test
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public int TestResult { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserID { get; set; }
    }
}
