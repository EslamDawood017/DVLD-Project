using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class LicenseClassService : ILicenseClassService
    {
        private readonly ILicenseClassRepository _licenseClassRepository;

        public LicenseClassService(ILicenseClassRepository licenseClassRepository)
        {
            _licenseClassRepository = licenseClassRepository;
        }

        public int AddNewLicenseClass(LicenseClass licenseClass)
        {
            return _licenseClassRepository.AddNewLicenseClass(licenseClass);
        }

        public List<LicenseClass> GetLicenseClasses()
        {
            return _licenseClassRepository.getAllLicenseClass();
        }

        public LicenseClass GetLicenseClassInfoByClassName(string ClassName)
        {
            return _licenseClassRepository.GetLicenseClassInfoByClassName(ClassName);
        }

        public LicenseClass GetLicenseClassInfoByID(int LicenseClassID)
        {
            return _licenseClassRepository.GetLicenseClassInfoByID(LicenseClassID);
        }

        public bool UpdateLicenseClass(LicenseClass licenseClass)
        {
            return _licenseClassRepository.UpdateLicenseClass(licenseClass);
        }
        
    }
}
