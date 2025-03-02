using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IDetainedRepository
    {
        public DetailedLicense GetDetainedLicenseInfoByID(int detainId);

        public DetailedLicense GetDetainedLicenseInfoByLicenseID(int licenseID);

        public List<DetainedLicenseDto> GetAllDetainedLicenses();

        public int AddNewDetainedLicense(NewDetainDto requist);

        public bool UpdateDetainedLicense(NewDetainDto requist);

        public bool ReleaseDetainedLicense(int LicenseId, int ReleasedByUserId, int ReleaseApplicationID);

        public bool IsLicenseDetained(int licenseID);
    }
}
