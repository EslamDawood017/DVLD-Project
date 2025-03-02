using DVLD_BusinessLayer.Enums;
using DVLD_BusinessLayer.interfaces;
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
    public class DetainedLicenseService : IDetainedLicenseService
    {
        private readonly IDetainedRepository detainedRepository;
        private readonly IApplicationRepository applicationRepository;
        private readonly IApplicationTypeRepository applicationTypeRepository;
        private readonly ILicenseService licenseService;
        private readonly IPersonRepository personRepository;

        public DetainedLicenseService(IDetainedRepository detainedRepository ,
            IApplicationRepository applicationRepository ,
            IApplicationTypeRepository applicationTypeRepository ,
            ILicenseService licenseService , IPersonRepository personRepository)
        {
            this.detainedRepository = detainedRepository;
            this.applicationRepository = applicationRepository;
            this.applicationTypeRepository = applicationTypeRepository;
            this.licenseService = licenseService;
            this.personRepository = personRepository;
        }
        public int AddNewDetainedLicense(NewDetainDto detainDto)
        {
            return detainedRepository.AddNewDetainedLicense(detainDto);
        }

        public List<DetainedLicenseDto> GetAllDetainedLicenses()
        {
            return detainedRepository.GetAllDetainedLicenses();
        }

        public DetailedLicense GetDetainedLicenseInfoByID(int detainId)
        {
            return detainedRepository.GetDetainedLicenseInfoByID(detainId);
        }

        public DetailedLicense GetDetainedLicenseInfoByLicenseID(int licenseID)
        {
            return detainedRepository.GetDetainedLicenseInfoByLicenseID(licenseID);
        }

        public bool IsLicenseDetained(int licenseID)
        {
            return detainedRepository.IsLicenseDetained(licenseID);
        }

        public bool ReleaseDetainedLicense(int LicenseId, int ReleasedByUserId)
        {
            var License = licenseService.GetLicenseInfoDToByLicenseId(LicenseId);

            var Person = personRepository.GetPersonByNationalNo(License.NationalNo);

            var ApplicationType = applicationTypeRepository.GetApplicationTypeInfoById((int)enApplicationType.ReleaseDetainedDrivingLicsense);


            int ReleasedApplicationId = applicationRepository.CreateNewApplication(new Application
            {
                ApplicationID = 0 ,
                ApplicantPersonID = Person.PersonID ,
                ApplicationStatus = (int)enStatus.New,
                ApplicationTypeID = (int)enApplicationType.ReleaseDetainedDrivingLicsense,
                CreatedByUserID = ReleasedByUserId,
                PaidFees = ApplicationType.ApplicationFees,
                LastStatusDate = DateTime.Now,
                ApplicationDate = DateTime.Now,
            });

            return detainedRepository.ReleaseDetainedLicense(LicenseId , ReleasedByUserId , ReleasedApplicationId);
        }

        public bool UpdateDetainedLicense(NewDetainDto detainDto)
        {
            return detainedRepository.UpdateDetainedLicense(detainDto);
        }
    }
}
