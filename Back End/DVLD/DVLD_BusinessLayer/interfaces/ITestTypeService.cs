using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ITestTypeService
    {
        public TestType GetTestTypeInfoById(int TestTypeId);
        public List<TestType> GetAllTestTypes();
        public bool UpdateTestType(TestType Testtype);
        public int AddNewTestType(TestType Testtype);
    }
}
