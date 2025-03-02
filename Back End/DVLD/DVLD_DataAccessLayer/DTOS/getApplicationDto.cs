using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class getApplicationDto
    {
        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }  // New, Cancelled, or Completed
    }
}
