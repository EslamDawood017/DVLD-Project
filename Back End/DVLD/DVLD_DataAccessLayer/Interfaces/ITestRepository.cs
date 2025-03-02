using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface ITestRepository
    {

        public Test GetTestInfoByID(int TestID);
        public  TestDto GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID);
        public List<Test> GetAllTests();
        public int AddNewTest(Test newTest);
        public bool UpdateTest(Test updatedTest);
        public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID);
    }
}
