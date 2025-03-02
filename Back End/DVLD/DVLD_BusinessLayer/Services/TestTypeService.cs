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
    public class TestTypeService : ITestTypeService
    {
        private readonly ITestTypeRepository _testTypeRepository;

        public TestTypeService(ITestTypeRepository testTypeRepository )
        {
            _testTypeRepository = testTypeRepository;
        }
        public int AddNewTestType(TestType Testtype)
        {
            return _testTypeRepository.AddNewTestType(Testtype);
        }

        public List<TestType> GetAllTestTypes()
        {
            return _testTypeRepository.GetAllTestTypes();
        }

        public TestType GetTestTypeInfoById(int TestTypeId)
        {
            return _testTypeRepository.GetTestTypeInfoById(TestTypeId);
        }

        public bool UpdateTestType(TestType Testtype)
        {
            return _testTypeRepository.UpdateTestType(Testtype);
        }
    }
}
