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
    public interface ILicenseClassRepository
    {
        public List<LicenseClass> getAllLicenseClass();
        public LicenseClass GetLicenseClassInfoByID(int LicenseClassID);
        public LicenseClass GetLicenseClassInfoByClassName(string ClassName);
        public int AddNewLicenseClass(LicenseClass licenseClass);
        public bool UpdateLicenseClass(LicenseClass licenseClass);
       
    }
}
