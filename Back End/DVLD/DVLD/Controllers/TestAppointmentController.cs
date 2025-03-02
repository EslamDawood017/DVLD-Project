using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestAppointmentController : ControllerBase
    {
        private readonly ITestTypeService testTypeService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;
        private readonly IUserService userService;
        private readonly ITestAppointmentService testAppointmentService;

        public TestAppointmentController(ITestTypeService testTypeService , 
            ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService , 
            IUserService userService , 
            ITestAppointmentService testAppointmentService)
        {
            this.testTypeService = testTypeService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
            this.userService = userService;
            this.testAppointmentService = testAppointmentService;
        }
        [HttpPost("AddNewTestAppointment")]
        public IActionResult AddNewTestAppointment(TestAppointment testAppointment)
        {
            if (testTypeService.GetTestTypeInfoById(testAppointment.TestTypeID) == null)
                return BadRequest($"No Test Type with id : {testAppointment.TestTypeID}");

            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(testAppointment.LocalDrivingLicenseApplicationID)) 
                return BadRequest($"No Local Driving License Application With Id {testAppointment.LocalDrivingLicenseApplicationID}");

            if (!userService.IsUserExist(testAppointment.CreatedByUserID))
                return BadRequest($"No User Found With Id {testAppointment.CreatedByUserID}");

            var testAppointmentId = testAppointmentService.AddNewTestAppointment(testAppointment);

            if(testAppointmentId > 0 )
                return Ok(new {id = testAppointmentId});

            return BadRequest("Error try again later");

        }
        [HttpGet("getAllTestAppointment")]
        public IActionResult getAllTestAppointment()
        {
            var result = testAppointmentService.GetTestAppointmentList();

            if (result == null)
                return NotFound("No Test Appointment Found");

            return Ok(result);
        }
        [HttpGet("getTestAppintmentPerTestType")]
        public IActionResult getTestAppintmentPerTestType(int LocalDrivingLicenseApplicationId , int TestType)
        {

            if (testTypeService.GetTestTypeInfoById(TestType) == null)
                return BadRequest($"No Test Type with id : {TestType}");

            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationId))
                return BadRequest($"No Local Driving License Application With Id {LocalDrivingLicenseApplicationId}");

            var result = testAppointmentService.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationId, TestType);

            if (result == null)
                return NotFound("No Test Appointment Found");

            return Ok(result);
        }
        [HttpPost("UpdateTestAppointmentDate")]
        public IActionResult UpdateTestAppointmentDate(int TestAppointmentId ,  DateTime NewTestAppointmentDate)
        {
            var result = testAppointmentService.UpdateTestAppointmentDate(TestAppointmentId , NewTestAppointmentDate);

            if (!result)
                return BadRequest("Not Updated");

            return Ok(result);
        }
        [HttpGet("getLastTestAppointment")]
        public IActionResult GetLastTestAppointmentPerTestType(int LocalDrivingLicenseApplicationId, int TestTypeId)
        {
            if (testTypeService.GetTestTypeInfoById(TestTypeId) == null)
                return BadRequest($"No Test Type with id : {TestTypeId}");

            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationId))
                return BadRequest($"No Local Driving License Application With Id {LocalDrivingLicenseApplicationId}");

            var Result = testAppointmentService.GetLastTestAppointment(LocalDrivingLicenseApplicationId, TestTypeId);

            if (Result == null)
                return NotFound();

            return Ok(Result);

        }
    }
}
