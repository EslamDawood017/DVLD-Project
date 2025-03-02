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
    public class TestServicecs : ITestService
    {
        private readonly ITestRepository testRepository;

        public TestServicecs(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public int AddNewTest(Test test)
        {
            return this.testRepository.AddNewTest(test);
        }

        public TestDto GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            return this.testRepository.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return this.testRepository.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
    }
}
