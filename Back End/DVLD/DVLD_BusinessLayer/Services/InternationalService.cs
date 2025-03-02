using DVLD_BusinessLayer.Enums;
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
    public class InternationalService : IInternationalService
    {
        private readonly IInternationalLicenseRepository internationalLicenseRepository;
        private readonly IApplicationRepository applicationRepository;
        private readonly ILicenseRepository licenseRepository;
        private readonly IPersonRepository personRepository;
        private readonly IApplicationTypeRepository applicationTypeRepository;
        private readonly ILicenseClassRepository licenseClassRepository;

        public InternationalService(IInternationalLicenseRepository internationalLicenseRepository , 
            IApplicationRepository applicationRepository , 
            ILicenseRepository licenseRepository ,
            IPersonRepository personRepository , 
            IApplicationTypeRepository applicationTypeRepository , 
            ILicenseClassRepository licenseClassRepository)
        {
            this.internationalLicenseRepository = internationalLicenseRepository;
            this.applicationRepository = applicationRepository;
            this.licenseRepository = licenseRepository;
            this.personRepository = personRepository;
            this.applicationTypeRepository = applicationTypeRepository;
            this.licenseClassRepository = licenseClassRepository;
        }
        public int CreateNewInternationalLicense(int LicenseId, int CreatedByUserId)
        {
            var License = licenseRepository.GetLicenseInfoDToByLicenseId(LicenseId);

            var LicenseClassInfo = licenseClassRepository.GetLicenseClassInfoByClassName(License.ClassName);

            var Person = personRepository.GetPersonByNationalNo(License.NationalNo);

            var ApplicationType = applicationTypeRepository.GetApplicationTypeInfoById((int)enApplicationType.NewInternationalLicense);

            var InternationalApplicationId = applicationRepository.CreateNewApplication(new DVLD_DataAccessLayer.Models.Application
            {
                ApplicationID = 0,
                ApplicantPersonID = Person.PersonID ,
                ApplicationTypeID = (int)enApplicationType.NewInternationalLicense,
                ApplicationDate = DateTime.Now,
                LastStatusDate = DateTime.Now,
                ApplicationStatus = (int)enStatus.New,
                CreatedByUserID = CreatedByUserId ,
                PaidFees = ApplicationType.ApplicationFees

            });

            var InterNationalLicenseID = internationalLicenseRepository.AddNewInternationalLicense(new InternationalLicense
            {
                InternationalLicenseID = 0 ,
                ApplicationID = InternationalApplicationId ,
                CreatedByUserID= CreatedByUserId ,
                IsActive = true ,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength),
                DriverID = License.DriverID,
                IssuedUsingLocalLicenseID = License.LicenseID
            });

            return InterNationalLicenseID;
        }
        public List<InternationalLicense> GetAllInternationalLicenses()
        {
            return internationalLicenseRepository.GetAllInternationalLicenses(); 
        }
    }
}
