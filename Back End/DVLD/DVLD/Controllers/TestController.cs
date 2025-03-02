
using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService testService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;

        public TestController(ITestService testService , ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService)
        {
            this.testService = testService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
        }
        [HttpGet("PassedTest")]
        public IActionResult GetNumberOfPassedTest(int localDrivingLicenseApplication)
        {
            var PassedTestNumber = testService.GetPassedTestCount(localDrivingLicenseApplication);

            return Ok(PassedTestNumber);
        }
        [HttpPost("addNew")]
        public IActionResult AddNewTest(Test test)
        {
            var res = testService.AddNewTest(test);

            if(res > 0 )
                return Ok(res);

            return BadRequest();
        }
        [HttpGet("GetLastTestByPersonAndTestTypeAndLicenseClass")]
        public IActionResult GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            

            var result = testService.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, TestTypeID);

            if(result == null )
                return NotFound();

            return Ok(result.TestResult);
        }

    }
}
