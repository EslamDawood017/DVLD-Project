using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ITestService
    {
        public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID);
        public int AddNewTest(Test test);
        public TestDto GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID);
    }
}
