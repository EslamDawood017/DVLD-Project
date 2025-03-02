using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ILocalDrivingLicenseApplicationService
    {
        public int CreateNewLocalDrivingLicenseApplication(CreateLocalDrivingLicenseApplicationDto dto);
        public getApplicationDto GetApplicationInfoByLocalDrivingLicenseId(int LocalDrivingLicesneAppId);
        public bool isThereAnActiveApplication(int personId, int LicenseClassId);
        public List<getLocalDrivingLicenseApplicationDto> GetAllLocalDrivingLicenseApplications();
        public int GetPersonIdForLocalDrivingLicense(int LocalDrivingLicenseApplicationID);
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsById(int LocalDrivingLicenseApplicationID);
        public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID);
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseById(int LocalDrivingLicenseApplicationID);
        public bool UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application);
        public bool IsLocalDrivingLicenseAppExist(int id);
        public bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public int TotalTrailPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
    }
}
