using DVLD_BusinessLayer.Enums;
using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
       
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository licenseRepository;
        private readonly IDriverService driverService;
        private readonly IApplicationService applicationService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;
        private readonly IPersonService personService;
        private readonly ILicenseClassService licenseClassService;

        public LicenseService(ILicenseRepository licenseRepository ,
            ILicenseClassService localDrivingLicenseClassService,
            IDriverService driverService,
            IApplicationService applicationService,
            ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService,
            IPersonService personService, ILicenseClassService licenseClassService)
        {
            this.licenseRepository = licenseRepository;
            this.driverService = driverService;
            this.applicationService = applicationService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
            this.personService = personService;
            this.licenseClassService = licenseClassService;
        }
        public License GetLicecnseInfoById(int LicenseID)
        {
            return this.licenseRepository.GetLicecnseInfoById(LicenseID);
        }
        public List<License> GetAllLicenses()
        {
            return this.licenseRepository.GetAllLicenses();
        }
        public int AddNewLicense(License license)
        {
            return this.licenseRepository.AddNewLicense(license);
        }
        public bool UpdateLicense(License license)
        {
            return this.UpdateLicense(license);
        }
        public int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return this.licenseRepository.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        public bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }
        public bool DeactivateLicense(int LicenseID)
        {
            return this.licenseRepository.DeactivateLicense(LicenseID);
        }

        public int IssueLicenseForFirstTime(IssueLicenseDto requist)
        {
            int DriverId = -1;

            var LocalDrivingLicenseApplication = localDrivingLicenseApplicationService.GetLocalDrivingLicenseApplicationsById(requist.LocalDrivingLicenseApplicationId);

            var Application = localDrivingLicenseApplicationService.GetApplicationInfoByLocalDrivingLicenseId(requist.LocalDrivingLicenseApplicationId);

            var PersonId = localDrivingLicenseApplicationService.GetPersonIdForLocalDrivingLicense(requist.LocalDrivingLicenseApplicationId);

            if (GetActiveLicenseIDByPersonID(PersonId, LocalDrivingLicenseApplication.LicenseClassID) > 0)
                return -100;

            var Driver = driverService.GetDriverInfoByPersonID(PersonId);

            if (Driver == null)
                DriverId = driverService.AddNewDriver(PersonId, requist.CreatedByUserId);
            else
                DriverId = Driver.DriverID;


            var LicenseClassId = LocalDrivingLicenseApplication.LicenseClassID;

            var LicenseClass = licenseClassService.GetLicenseClassInfoByID(LicenseClassId);

            int LicenseId = licenseRepository.AddNewLicense(new License
            {
                LicenseID = 0 ,               
                ApplicationID = Application.ApplicationID,
                CreatedByUserID = requist.CreatedByUserId,
                DriverID = DriverId,
                IsActive = true,
                Notes = requist.Note , 
                PaidFees = LicenseClass.ClassFees,
                IssueReason = (int)enIssueReason.FirstTime ,
                LicenseClassID = LicenseClassId,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(LicenseClass.DefaultValidityLength),
            });

            if (LicenseId > 0)
                this.applicationService.UpdateStatus(Application.ApplicationID, (int)enStatus.Completed);

            return LicenseId;
        }

        public LicenseInfoDto GetLicenseInfo(int LocalDrivingLicenseApplicationId)
        {
            return licenseRepository.GetLicenseInfo(LocalDrivingLicenseApplicationId);
        }
        public LicenseInfoDto GetLicenseInfoDToByLicenseId(int LicenseId)
        {
            return licenseRepository.GetLicenseInfoDToByLicenseId(LicenseId);
        }

        public int RenewLicense(int LicenseID, int CreatedByUserId ,string Note)
        {
            var License = this.licenseRepository.GetLicenseInfoDToByLicenseId(LicenseID);

            var Person = this.personService.GetPersonByNationalNo(License.NationalNo);

            var LicenseClass = this.licenseClassService.GetLicenseClassInfoByClassName(License.ClassName);

            int RenewLicenseApplicationId = this.applicationService.CreateNewApplication(new CreateAppDto
            {
                ApplicationTypeId = (int)enApplicationType.RenewDrivingLicenseService,
                PersonId = Person.PersonID,
                CreatedByUserId = CreatedByUserId
            });

            this.licenseRepository.DeactivateLicense(LicenseID);

            int RenewedLicenseId = this.licenseRepository.AddNewLicense(new License
            {
                LicenseID = 0,
                ApplicationID = RenewLicenseApplicationId,
                CreatedByUserID = CreatedByUserId,
                DriverID = License.DriverID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(LicenseClass.DefaultValidityLength),
                IsActive = true,
                IssueReason = (int)enIssueReason.Renew , 
                LicenseClassID = LicenseClass.LicenseClassID,
                Notes = Note,
                PaidFees = LicenseClass.ClassFees
            });

            return RenewedLicenseId;
        }
        public int ReplaceForLostOrDamageLicense(IssueForLostOrDamageDto requist)
        {
            var License = this.licenseRepository.GetLicenseInfoDToByLicenseId(requist.LicenseId);

            var Person = this.personService.GetPersonByNationalNo(License.NationalNo);

            var LicenseClass = this.licenseClassService.GetLicenseClassInfoByClassName(License.ClassName);

            int LostOrDamageApplicationId = this.applicationService.CreateNewApplication(new CreateAppDto
            {
                ApplicationTypeId = requist.ReasonId,
                PersonId = Person.PersonID,
                CreatedByUserId = requist.CreatedByUserId
            });

            this.licenseRepository.DeactivateLicense(requist.LicenseId);

            int NewLicenseId = this.licenseRepository.AddNewLicense(new License
            {
                LicenseID = 0,
                ApplicationID = LostOrDamageApplicationId,
                CreatedByUserID = requist.CreatedByUserId,
                DriverID = License.DriverID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(LicenseClass.DefaultValidityLength),
                IsActive = true,
                IssueReason = requist.ReasonId,
                LicenseClassID = LicenseClass.LicenseClassID,
                PaidFees = LicenseClass.ClassFees,
                Notes = requist.Note

            });

            return NewLicenseId;
        }
        public List<LicenseInfoDto> GetAllLicenseForPerosnByNationalNo(string NationalNo)
        {
            return licenseRepository.GetAllLicenseForPerosnByNationalNo(NationalNo);
        }
    }
   
}
