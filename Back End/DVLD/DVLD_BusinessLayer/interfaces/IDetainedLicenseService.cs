using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IDetainedLicenseService
    {
        public DetailedLicense GetDetainedLicenseInfoByID(int detainId);

        public DetailedLicense GetDetainedLicenseInfoByLicenseID(int licenseID);

        public List<DetainedLicenseDto> GetAllDetainedLicenses();

        public int AddNewDetainedLicense(NewDetainDto detainDto);

        public bool UpdateDetainedLicense(NewDetainDto detainDto);

        public bool ReleaseDetainedLicense(int LicenseId, int ReleasedByUserId);

        public bool IsLicenseDetained(int licenseID);
    }
}
