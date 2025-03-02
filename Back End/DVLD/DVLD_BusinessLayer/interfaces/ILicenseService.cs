using System;
using System.Collections.Generic;
using DVLD_DataAccessLayer.Models;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using DVLD_DataAccessLayer.DTOS;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ILicenseService
    {
        public LicenseInfoDto GetLicenseInfo(int LocalDrivingLicenseApplicationId);
        public License GetLicecnseInfoById(int LicenseID);
        public List<License> GetAllLicenses();
        public int AddNewLicense(License license);
        public bool UpdateLicense(License license);
        public int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID);
        public bool DeactivateLicense(int LicenseID);
        public int IssueLicenseForFirstTime(IssueLicenseDto requist);
        public bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID);
        public LicenseInfoDto GetLicenseInfoDToByLicenseId(int LicenseId);
        public int RenewLicense(int LicenseID, int CreatedByUserId, string Note);
        int ReplaceForLostOrDamageLicense(IssueForLostOrDamageDto requist);
        public List<LicenseInfoDto> GetAllLicenseForPerosnByNationalNo(string NationalNo);
    }
}
