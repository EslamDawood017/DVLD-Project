using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternationalLicenseController : ControllerBase
    {
        private readonly ILicenseService licenseService;
        private readonly IInternationalService internationalService;

        public InternationalLicenseController(ILicenseService licenseService ,
            IInternationalService internationalService)
        {
            this.licenseService = licenseService;
            this.internationalService = internationalService;
        }
        [HttpPost("Add/{LicenseId}")]
        public IActionResult AddNewInternationalLicense(int LicenseId , int CreatedByUserId)
        {
            var LocalLicense = licenseService.GetLicecnseInfoById(LicenseId);

            if (LocalLicense == null) 
                return NotFound(new {message = "No Local License Found with this id.. "});
            
            if(LocalLicense.LicenseClassID != 3  )
                return BadRequest(new { message = "selected license should be class 3 , select another one..." });    
            
            var NewId = internationalService.CreateNewInternationalLicense(LicenseId , CreatedByUserId);
            
            if(NewId > 0)
                return Ok(NewId);

            return BadRequest("No Created Try angain later");
        
        }
        [HttpGet("All")]
        public IActionResult GetAllInternationalLicenes()
        {
            var result = internationalService.GetAllInternationalLicenses();

            return Ok(result);
        }

    }
}
