using System;
using System.Collections.Generic;
using DVLD_DataAccessLayer.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface ILicenseRepository
    {
        public License GetLicecnseInfoById(int LicenseID);
        public List<License> GetAllLicenses();
        public int AddNewLicense(License license);
        public bool UpdateLicense(License license);
        public int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID);
        public bool DeactivateLicense(int LicenseID);
        public LicenseInfoDto GetLicenseInfo(int LocalDrivingLicenseApplicationId);
        public LicenseInfoDto GetLicenseInfoDToByLicenseId(int LicenseId);
        public List<LicenseInfoDto> GetAllLicenseForPerosnByNationalNo(string NationalNo);

    }
}
