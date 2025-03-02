using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IPersonService personService;
        private readonly IUserService userService;
        private readonly IAppTypeService appTypeService;
        private readonly ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService;

        public ApplicationController(IApplicationService applicationService, IPersonService personService,
            IUserService userService, IAppTypeService appTypeService , ILocalDrivingLicenseApplicationService localDrivingLicenseApplicationService)
        {
            _applicationService = applicationService;
            this.personService = personService;
            this.userService = userService;
            this.appTypeService = appTypeService;
            this.localDrivingLicenseApplicationService = localDrivingLicenseApplicationService;
        }
        [HttpPost("Addnew")]
        public IActionResult CreateNewApplication(CreateAppDto application)
        {
            //person validation
            if (!personService.isPersonExistById(application.PersonId))
                return NotFound($"No Person Found With Id : {application.PersonId}");

            if (!userService.IsUserExist(application.CreatedByUserId))
                return NotFound($"No User Found with Id : {application.CreatedByUserId}");

            var appType = appTypeService.GetApplicationTypeInfoById(application.ApplicationTypeId);

            if (appType == null)
                return NotFound($"No Application Type Found with Id : {application.ApplicationTypeId}");


            int ApplicationId = _applicationService.CreateNewApplication(application);

            if (ApplicationId == -1)
                return BadRequest("Error invalid data try angain");

            return Ok(ApplicationId);

        }
        [HttpGet("{id}")]
        public IActionResult GetApplicationById(int id)
        {
            var application = _applicationService.GetApplicationById(id);

            if (application == null)
                return NotFound($"No Application With Id : {id}");

            return Ok(application);
        }
        [HttpGet("getAll")]
        public IActionResult getAllApplications()
        {
            var applications = _applicationService.GetAllApplications();

            if (applications == null)
                return NotFound("No Application Was found");

            return Ok(applications);
        }
        [HttpPut("UpdateApplication")]
        public IActionResult UpdateApplication(Application application)
        {
            if (_applicationService.GetApplicationById(application.ApplicationID) == null)
                return NotFound($"No Application Found With Id : {application.ApplicationID}");


            if (!personService.isPersonExistById(application.ApplicantPersonID))
                return NotFound($"No Person Found With Id : {application.ApplicantPersonID}");

            if (!userService.IsUserExist(application.CreatedByUserID))
                return NotFound($"No User Found with Id : {application.CreatedByUserID}");

            var appType = appTypeService.GetApplicationTypeInfoById(application.ApplicationTypeID);

            if (appType == null)
                return NotFound($"No Application Type Found with Id : {application.ApplicationTypeID}");

            var result = _applicationService.UpdateApplication(application);

            if (!result)
                return BadRequest("error item not updated");

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            if (_applicationService.GetApplicationById(id) == null)
                return NotFound($"No Application Found with id : {id}");

            var result = _applicationService.DeleteApplication(id);

            if (!result)
                return BadRequest("error application Not Delete");

            return Ok(new { result = result });
        }
        [HttpGet("isApplicationExist/{id}")]
        public IActionResult isApplicationExist(int id)
        {
            var result = _applicationService.isApplicationExist(id);

            if (!result)
                return NotFound(new { msg = result });

            return Ok(new { msg = result });
        }
        [HttpGet("getActiveApplication")]
        public IActionResult getActiveApplication(int PersonId, int ApplicationTypeID)
        {
            if (!personService.isPersonExistById(PersonId))
                return BadRequest($"No Person Found With Id : {PersonId}");

            var AppType = appTypeService.GetApplicationTypeInfoById(ApplicationTypeID);

            if (AppType == null)
                return BadRequest($"No Application Type {ApplicationTypeID}");

            var Result = _applicationService.DoesPersonHaveActiveApplication(PersonId, ApplicationTypeID);

            if (!Result)
                return NotFound(new { Result = Result });

            return Ok(new { Result = Result });

        }
        [HttpPut("UpdateStatusByApplicationId")]
        public IActionResult UpdateStatusByApplicationId(int ApplicationId , int NewStatus )
        {
            if (!_applicationService.isApplicationExist(ApplicationId))
                return BadRequest($"No Application Found with Id : {ApplicationId}");

            var result = _applicationService.UpdateStatus(ApplicationId, NewStatus);

            if (!result)
                return BadRequest("Not Updated");

            return Ok(new { result });
        }
        [HttpPut("UpdateStatusByLocalDrivingLicenseAppId")]
        public IActionResult UpdateStatusByLocalDrivingLicenseAppId(int LocalDrivingLicenseAppId, int NewStatus)
        {
            var application = localDrivingLicenseApplicationService.GetApplicationInfoByLocalDrivingLicenseId(LocalDrivingLicenseAppId);
            if (application == null)
                return BadRequest($"No Application Found ");

            var result = _applicationService.UpdateStatus(application.ApplicationID, NewStatus);

            if (!result)
                return BadRequest("Not Updated");

            return Ok(new { result });
        }
    }
}
