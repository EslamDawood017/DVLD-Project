using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppTypeController : ControllerBase
    {
        private readonly IAppTypeService _appTypeService;

        public AppTypeController(IAppTypeService appTypeService)
        {
            _appTypeService = appTypeService;
        }
        [HttpGet("getAll")]
        public IActionResult GetAllAppType()
        {
            var appTypes = _appTypeService.GetAllApplicationTypes();

            if(!appTypes.Any()) 
                return NotFound("No Application Type Found in the system");

            return Ok(appTypes);
        }
        [HttpGet("{id}")]
        public IActionResult GetAppTypeById([FromRoute]int id)
        {
            var appType = _appTypeService.GetApplicationTypeInfoById(id);

            if(appType == null)
                NotFound($"No Application Type with Id : {id}");

            return Ok(appType);
        }
        [HttpPost("addNew")]
        public IActionResult AddNewApplicationType([FromBody]ApplicationType applicationType)
        {
            var AppTypeId = _appTypeService.AddNewApplicationType(applicationType);

            if(AppTypeId != -1  )
                return Ok(AppTypeId);

            return BadRequest("Enter valid Data");
        }
        [HttpPut("Update")]
        public IActionResult UpdateAppType(ApplicationType applicationType)
        {
            var appType = _appTypeService.GetApplicationTypeInfoById(applicationType.ApplicationTypeID);

            if (appType == null)
                return NotFound($"No Application Type with Id : {applicationType.ApplicationTypeID}");

            var result = _appTypeService.UpdateApplicationType(applicationType);

            if (!result)
                return BadRequest("enter a valid data");

            return Ok(new { update = result });

        }
    }
}
