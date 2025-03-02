using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Mapper
{
    public static class LocalDrivingLicenseAppMapper
    {
        public static LocalDrivingLicenseApplications GetLocalDrivingLicenseApplications(int ApplicationId , int LicenseClass)
        {
            return new LocalDrivingLicenseApplications
            {
                ApplicationID = ApplicationId,
                LicenseClassID = LicenseClass
            };
        }
    }
}
