using DVLD_BusinessLayer.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVLD_DataAccessLayer.Models;
using DVLD_DataAccessLayer.DTOS;
using DVLD_BusinessLayer.Services;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService licenseService;
        private readonly ITestService testService;
        private readonly IDriverService driverService;
        private readonly IApplicationService applicationService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;
        private readonly IPersonService personService;
        private readonly ILicenseClassService licenseClassService;

        public LicenseController(ILicenseService licenseService,
            ILicenseClassService localDrivingLicenseClassService,
            ITestService testService,
            IDriverService driverService,
            IApplicationService applicationService,
            ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService,
            IPersonService personService, ILicenseClassService licenseClassService)
        {
            this.licenseService = licenseService;
            this.testService = testService;
            this.driverService = driverService;
            this.applicationService = applicationService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
            this.personService = personService;
            this.licenseClassService = licenseClassService;
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetLicenseById(int id)
        {
            var result = this.licenseService.GetLicecnseInfoById(id);

            if (result == null)
                return NotFound($"No License found with id {id}");

            return Ok(result);
        }
        [HttpGet("GetAllLicense")]
        public IActionResult GetAllLicense()
        {
            var result = this.licenseService.GetAllLicenses();

            if (result == null) return NotFound("No Result Found");

            return Ok(result);
        }
        [HttpPost("AddNew")]
        public IActionResult AddNewLicense(License license)
        {
            int InsertedId = this.licenseService.AddNewLicense(license);

            if (InsertedId > 0)
                return Ok(InsertedId);

            return BadRequest(ModelState.Values);
        }
        [HttpPost("Update")]
        public IActionResult UpdateLicense(License license)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var result = this.licenseService.UpdateLicense(license);

            if (result)
                return Ok(result);

            return BadRequest("Not Updated");

        }
        [HttpGet("GetActiveLicensePerPerson")]
        public IActionResult GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            if (!personService.isPersonExistById(PersonID))
                return NotFound("No Peron found with this id");

            var ActiveLicenseId = licenseService.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

            if (ActiveLicenseId > 0)
                return Ok(ActiveLicenseId);

            return BadRequest();

        }
        [HttpPut("DeActivateLicense/{LiceneseId}")]
        public IActionResult DeActivateLicense(int LiceneseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            if (this.licenseService.GetLicecnseInfoById(LiceneseId) == null)
                return NotFound("No License Was Found with this id");

            var res = licenseService.DeactivateLicense(LiceneseId);

            if (res)
                return Ok(res);

            return BadRequest();

        }
        [HttpGet("IsLicenseExistByPersonID")]
        public IActionResult IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            var result = licenseService.IsLicenseExistByPersonID(PersonID, LicenseClassID);

            return Ok(new { result = result });
        }
        [HttpGet("IsLicenseExistByLocalDrivingLicenseAppID")]
        public IActionResult IsLicenseExistByLocalDrivingLicenseAppID(int LocalDrivingLicenseAppID, string LicenseClassName)
        {

            var PersonId = localDrivingLicenseApplicationService.GetPersonIdForLocalDrivingLicense(LocalDrivingLicenseAppID);

            var LicenseClass = licenseClassService.GetLicenseClassInfoByClassName(LicenseClassName);

            var result = false;

            if (PersonId > 0)
                result = licenseService.IsLicenseExistByPersonID(PersonId, LicenseClass.LicenseClassID);

            return Ok(result);
        }
        [HttpPost("IssueForFirstTime")]
        public IActionResult IssueForFirstTime(IssueLicenseDto requist)
        {
            int NumberOfPassedTest = testService.GetPassedTestCount(requist.LocalDrivingLicenseApplicationId);

            var localDrivingLicenseApplication = localDrivingLicenseApplicationService.GetLocalDrivingLicenseApplicationsById(requist.LocalDrivingLicenseApplicationId);

            if (localDrivingLicenseApplication == null)
                return NotFound($"No Local Driving License Application with id = {requist.LocalDrivingLicenseApplicationId}");

            if (NumberOfPassedTest < 3)
                return BadRequest("You must pass all test !! ");

            int InsertedId = licenseService.IssueLicenseForFirstTime(requist);

            if (InsertedId == -100)
                return BadRequest("this Person Is aleary have an active license");

            if (InsertedId > 0)
                return Ok(InsertedId);

            return BadRequest("No Issued enter a valid data");

        }
        [HttpGet("{LocalDrivingLicenseAppId}")]
        public IActionResult GetLicenseInfo(int LocalDrivingLicenseAppId)
        {
            var license = licenseService.GetLicenseInfo(LocalDrivingLicenseAppId);
            if (license == null)
            {
                return NotFound(new { message = "License not found" });
            }
            return Ok(license);
        }
        [HttpGet("LicenseId/{LicenseId}")]
        public IActionResult GetLicenseInfoByID(int LicenseId)
        {
            var license = licenseService.GetLicenseInfoDToByLicenseId(LicenseId);
            if (license == null)
            {
                return NotFound(new { message = "License not found" });
            }
            return Ok(license);
        }
        [HttpPost("RenewLicense/{LicenseId}")]
        public IActionResult RenewLicense(RenewLicenseDto requist)
        {
            var license = licenseService.GetLicenseInfoDToByLicenseId(requist.LicenseId);
            if (license == null)
                return NotFound(new { message = "License not found" });
            

            if(license.ExpirationDate >  DateTime.Now)
                return BadRequest(new { message = "This license is not expired .. !" });


            int NewLicenseId = licenseService.RenewLicense(requist.LicenseId, requist.CreatedByUserId, requist.Note);

            if(NewLicenseId > 0)
                return Ok(NewLicenseId);

            return BadRequest(new { message = "Not renewed try again" });
        }
        [HttpPost("ReplaceForLostOrDamageLicense")]
        public IActionResult ReplaceForLostOrDamageLicense(IssueForLostOrDamageDto requist)
        {
            var license = licenseService.GetLicenseInfoDToByLicenseId(requist.LicenseId);
            if (license == null)
                return NotFound(new { message = "License not found" });


            if (license.IsActive == "False")
                return BadRequest(new { message = "This license is not active .. !" });


            int NewLicenseId = licenseService.ReplaceForLostOrDamageLicense(requist);

            if (NewLicenseId > 0)
                return Ok(NewLicenseId);

            return BadRequest(new { message = "Not renewed try again" });
        }
        [HttpGet("All/Person/{NationalNo}")]
        public IActionResult GetAllLicensesForPerson(string NationalNo)
        {
            if (!personService.isPersonExistByNationalNo(NationalNo))
                return NotFound(new { msg = "No Person Found ..." });

            var licenses = licenseService.GetAllLicenseForPerosnByNationalNo(NationalNo);

            return Ok(licenses);
        }

    }
}
