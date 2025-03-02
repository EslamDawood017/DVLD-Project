using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Mapper;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class LocalDrivingLicenseApplicationService : ILocalDrivingLicenseApplicationService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IAppTypeService appTypeService;
        private readonly ILocalDrivingLicenseApplicationRepository localDrivingLicenseApplicationRepository;

        public LocalDrivingLicenseApplicationService(IApplicationRepository applicationRepository, IAppTypeService appTypeService , 
            ILocalDrivingLicenseApplicationRepository localDrivingLicenseApplicationRepository)
        {
            this.applicationRepository = applicationRepository;
            this.appTypeService = appTypeService;
            this.localDrivingLicenseApplicationRepository = localDrivingLicenseApplicationRepository;
        }
        public int CreateNewLocalDrivingLicenseApplication(CreateLocalDrivingLicenseApplicationDto dto)
        {
            var applicationType = appTypeService.GetApplicationTypeInfoById(1);

            var applicationInfo = dto.DtoToApplicationOfTypeLocalDrivingLicenseApplication(applicationType.ApplicationFees);

            var applicationId = applicationRepository.CreateNewApplication(applicationInfo);

            var LocalDrivingLicenseApplication = LocalDrivingLicenseAppMapper.GetLocalDrivingLicenseApplications(applicationId, dto.LisenceClassId);

            return localDrivingLicenseApplicationRepository.AddLocalDrivingLicenseApplication(LocalDrivingLicenseApplication);
        }

        public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            return localDrivingLicenseApplicationRepository.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
        }

        public bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return localDrivingLicenseApplicationRepository.DoesAttendTestType(LocalDrivingLicenseApplicationID , TestTypeID);
        }

        public List<getLocalDrivingLicenseApplicationDto> GetAllLocalDrivingLicenseApplications()
        {
            return localDrivingLicenseApplicationRepository.GetAllLocalDrivingLicenseApplications();
        }

        public LocalDrivingLicenseApplications GetLocalDrivingLicenseById(int LocalDrivingLicenseApplicationID)
        {
            return localDrivingLicenseApplicationRepository.GetLocalDrivingLicenseApplicationsById(LocalDrivingLicenseApplicationID);
        }

        public bool IsLocalDrivingLicenseAppExist(int id)
        {
            return GetLocalDrivingLicenseById(id) != null ? true : false;
        }

        public bool isThereAnActiveApplication(int personId, int LicenseClassId)
        {
            return localDrivingLicenseApplicationRepository.IsThereAnActiveApplication(personId , LicenseClassId);
        }

        public int TotalTrailPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return localDrivingLicenseApplicationRepository.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application)
        {
            return localDrivingLicenseApplicationRepository.UpdateLocalDrivingLicenseApplication(application);
        }
        public bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return this.localDrivingLicenseApplicationRepository.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public int GetPersonIdForLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            return localDrivingLicenseApplicationRepository.GetPersonIdForLocalDrivingLicense(LocalDrivingLicenseApplicationID);
        }
      
        public getApplicationDto GetApplicationInfoByLocalDrivingLicenseId(int LocalDrivingLicesneAppId)
        {
            return localDrivingLicenseApplicationRepository.GetApplicationInfoByLocalDrivingLicenseId(LocalDrivingLicesneAppId);

        }

        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsById(int LocalDrivingLicenseApplicationID)
        {
            return localDrivingLicenseApplicationRepository.GetLocalDrivingLicenseApplicationsById(LocalDrivingLicenseApplicationID);
        }
    }
}
