using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IInternationalLicenseRepository
    {
        public InternationalLicense GetInternationalLicenseInfoByID(int InternationalLicenseID);
        public List<InternationalLicense> GetAllInternationalLicenses();
        public List<InternationalLicense> GetDriverInternationalLicenses(int DriverID);
        public int AddNewInternationalLicense(InternationalLicense internationalLicense);
        public bool UpdateInternationalLicense(InternationalLicense internationalLicense);
        public int GetActiveInternationalLicenseIDByDriverID(int DriverID);
        

    }
}
