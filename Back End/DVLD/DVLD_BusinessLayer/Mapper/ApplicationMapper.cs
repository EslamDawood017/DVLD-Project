using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Mapper
{
    public static class ApplicationMapper
    {
        public static Application DtoToApplication(this CreateAppDto dto , decimal fees)
        {
            return new Application
            {
                ApplicantPersonID = dto.PersonId,
                ApplicationTypeID = dto.ApplicationTypeId,
                CreatedByUserID = dto.CreatedByUserId,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 1,
                LastStatusDate = DateTime.Now,
                PaidFees = fees,

            };
        }
        public static Application DtoToApplicationOfTypeLocalDrivingLicenseApplication (this CreateLocalDrivingLicenseApplicationDto dto , decimal fees)
        {
            return new Application
            {
                ApplicantPersonID = dto.PersonId,
                ApplicationTypeID = 1,
                CreatedByUserID = dto.CreatedByUserId,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 1,
                LastStatusDate = DateTime.Now,
                PaidFees = fees,

            };
        }
    }
}
