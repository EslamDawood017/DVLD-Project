using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface ILocalDrivingLicenseApplicationRepository
    {
        public int AddLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application);
        public getApplicationDto GetApplicationInfoByLocalDrivingLicenseId(int LocalDrivingLicesneAppId);
        public bool IsThereAnActiveApplication(int PersonId, int licenseClassId);
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsById(int LocalDrivingLicenseApplicationID);
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsByApplicationId(int ApplicationID);
        public List<getLocalDrivingLicenseApplicationDto> GetAllLocalDrivingLicenseApplications();
        public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID);
        public bool UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application);
        public int GetPersonIdForLocalDrivingLicense(int LocalDrivingLicenseApplicationID);
        public bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
    }
}
