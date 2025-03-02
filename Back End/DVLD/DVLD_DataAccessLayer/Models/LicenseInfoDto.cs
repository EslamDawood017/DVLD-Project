using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Models
{
    public class LicenseInfoDto
    {

        public string ClassName { get; set; }
        public string FullName { get; set; }
        public int LicenseID { get; set; }
        public string NationalNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string IssueReason { get; set; }
        public string Notes { get; set; }
        public string IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DriverID { get; set; }

            
        
    }
}
