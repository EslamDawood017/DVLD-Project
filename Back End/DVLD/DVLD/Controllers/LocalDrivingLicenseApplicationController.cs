using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocalDrivingLicenseApplicationController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IUserService userService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;
        private readonly ITestTypeService testTypeService;

        public LocalDrivingLicenseApplicationController(IPersonService personService , IUserService userService,
            ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService,
            ITestTypeService testTypeService)
        {
            this.userService = userService;
            this.personService = personService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
            this.testTypeService = testTypeService;
        }
        [HttpPost("AddNew")]
        public IActionResult AddNewLocalDrivingLicenseApplication(CreateLocalDrivingLicenseApplicationDto dto)
        {

            if (!personService.isPersonExistById(dto.PersonId))
                return NotFound($"No Person Found With Id : {dto.PersonId}");

            if (localDrivingLicenseApplicationService.isThereAnActiveApplication(dto.PersonId, dto.LisenceClassId))
                return BadRequest(new { msg = "there is an active application exist" });

            if (!userService.IsUserExist(dto.CreatedByUserId))
                return BadRequest($"No user Found with id : {dto.CreatedByUserId}");

            

            // add license class validation.


            var insertedId = localDrivingLicenseApplicationService.CreateNewLocalDrivingLicenseApplication(dto);

            if(insertedId > 0) 
                return Ok(insertedId);

            return BadRequest("Error inter a valid data and try again later");

        }
        [HttpGet("getAll")]
        public IActionResult getAllLocalDrivingLicenseApplications()
        {
            var result = localDrivingLicenseApplicationService.GetAllLocalDrivingLicenseApplications();

            if (result == null)
                return NotFound("no Local driving license application found");

            return Ok(result);
        }
        [HttpPut("update")]
        public IActionResult UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplications updatedApplication)
        {
            
            var result = localDrivingLicenseApplicationService.UpdateLocalDrivingLicenseApplication(updatedApplication);

            if(!result)
                return NotFound("Not Updated");

            return Ok(result);
        }
        [HttpDelete("{localDrivingLicenseApplicationId}")]
        public IActionResult DeleteLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId)
        {
            var result = localDrivingLicenseApplicationService.DeleteLocalDrivingLicenseApplication(localDrivingLicenseApplicationId);

            if (!result)
                return NotFound("Not deleted");

            return Ok(result);
        }
        [HttpGet("IsThereAnActiveApplication")]
        public IActionResult IsThereAnActiveApplication(int PersonId , int licenseClassId)
        {
            var result = localDrivingLicenseApplicationService.isThereAnActiveApplication(PersonId, licenseClassId);

            if (!result)
                return Ok( result);

            return NotFound(result);
        }
        [HttpGet("DoesAttendTestType")]
        public IActionResult DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationID))
                return NotFound($"No Local Driving License Found With Id {LocalDrivingLicenseApplicationID}");

            if (testTypeService.GetTestTypeInfoById(TestTypeID) == null)
                return NotFound($"No Test Type With Id {TestTypeID}");

            var result = localDrivingLicenseApplicationService.DoesAttendTestType(LocalDrivingLicenseApplicationID, TestTypeID);

            return Ok( result);
        }
        [HttpGet("TotalTrialPerTest")]
        public IActionResult GetTotalTrailPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationID))
                return NotFound($"No Local Driving License Found With Id {LocalDrivingLicenseApplicationID}");

            if (testTypeService.GetTestTypeInfoById(TestTypeID) == null)
                return NotFound($"No Test Type With Id {TestTypeID}");

            var result = localDrivingLicenseApplicationService.TotalTrailPerTest(LocalDrivingLicenseApplicationID, TestTypeID);

            return Ok( result );
        }
        [HttpGet("IsThereAnActiveScheduledTest")]
        public IActionResult IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            if (!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationID))
                return NotFound($"No Local Driving License Found With Id {LocalDrivingLicenseApplicationID}");

            if (testTypeService.GetTestTypeInfoById(TestTypeID) == null)
                return NotFound($"No Test Type With Id {TestTypeID}");

            var result = localDrivingLicenseApplicationService.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, TestTypeID);

            if(result)
                return BadRequest(new { result = result });

            return Ok(new { result = result });
        }
        [HttpGet("GetPersonIdForLocalDrivingLicense")]
        public IActionResult GetPersonIdForLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            if(!localDrivingLicenseApplicationService.IsLocalDrivingLicenseAppExist(LocalDrivingLicenseApplicationID))
                return NotFound($"No Local Driving License Application Found with Id => {LocalDrivingLicenseApplicationID  }");

            int personId = localDrivingLicenseApplicationService.GetPersonIdForLocalDrivingLicense(LocalDrivingLicenseApplicationID);

            if (personId > 0)
                return Ok(personId);

            return BadRequest("Error try again");
        }
        [HttpGet("getBaseApplicationForLocalDrivingLicenseApplication")]
        public IActionResult getBaseApplication(int LocalDrivingLicenseApplicationID)
        {
            if (localDrivingLicenseApplicationService.GetLocalDrivingLicenseById(LocalDrivingLicenseApplicationID) == null)
                return NotFound($"No Local driving License Application with id : {LocalDrivingLicenseApplicationID}");

            var result = localDrivingLicenseApplicationService.GetApplicationInfoByLocalDrivingLicenseId(LocalDrivingLicenseApplicationID);

            if(result == null)  
                return BadRequest("Error try again");

            return Ok(result);
        }
        [HttpGet("GetLocalDrivingLicenseAppById/{id}")]
        public IActionResult getLocalDrivingLicenseAppById(int id)
        { 

            var app = localDrivingLicenseApplicationService.GetLocalDrivingLicenseById(id);

            if (app == null)
                return NotFound($"No Local Driving License application found with id {id}");

            return Ok(app);
        }
    }
}
