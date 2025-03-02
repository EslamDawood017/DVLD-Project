using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ILicenseClassService
    {
        public List<LicenseClass> GetLicenseClasses();
        public LicenseClass GetLicenseClassInfoByID(int LicenseClassID);
        public LicenseClass GetLicenseClassInfoByClassName(string ClassName);
        public int AddNewLicenseClass(LicenseClass licenseClass);
        public bool UpdateLicenseClass(LicenseClass licenseClass);
    }
}
