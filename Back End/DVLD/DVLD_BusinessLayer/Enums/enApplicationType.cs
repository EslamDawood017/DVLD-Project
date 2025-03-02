using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Enums
{
    public enum enApplicationType
    {
        NewLocalDrivingLicenseService =  1 ,
        RenewDrivingLicenseService = 2 ,
        ReplacementforLostDrivingLicense = 3,
        ReplacementforDamagedDrivingLicense,
        ReleaseDetainedDrivingLicsense,
        NewInternationalLicense,
        RetakeTest
    }
}
