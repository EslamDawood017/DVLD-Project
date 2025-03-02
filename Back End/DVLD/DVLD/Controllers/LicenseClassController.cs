using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LicenseClassController : ControllerBase
    {
        private readonly ILicenseClassService _licenseClassService;

        public LicenseClassController(ILicenseClassService licenseClassService)
        {
            _licenseClassService = licenseClassService;
        }
        [HttpGet("getAll")]
        public IActionResult getAllLicenseClass()
        {
            var classes = _licenseClassService.GetLicenseClasses();

            if (classes == null) 
                return NotFound("No License Classes Found");

            return Ok(classes);
        }
        [HttpPost("add")]
        public IActionResult AddLicenseClass([FromBody] LicenseClass licenseClass)
        {
            if (licenseClass == null)
                return BadRequest(new { message = "Invalid license class data." });

            int newId = _licenseClassService.AddNewLicenseClass(licenseClass);

            return CreatedAtAction(nameof(GetLicenseClassById), new { licenseClassID = newId },
                new { message = "License class added successfully.", licenseClassID = newId });
        }
       
        [HttpGet("{licenseClassID}")]
        public IActionResult GetLicenseClassById(int licenseClassID)
        {
            var licenseClass = _licenseClassService.GetLicenseClassInfoByID(licenseClassID);

            if (licenseClass == null)
                return NotFound(new { message = "License class not found." });

            return Ok(licenseClass);
        }
        [HttpGet("exists/{className}")]
        public IActionResult CheckLicenseClassExists(string className)
        {
            var licenseClass = _licenseClassService.GetLicenseClassInfoByClassName(className);

            if (licenseClass == null)
                return NotFound(new { message = "License class not found." });

            return Ok(licenseClass);
        }
        [HttpPut("update")]
        public IActionResult UpdateLicenseClass(LicenseClass licenseClass)
        {
            if (licenseClass == null )
                return BadRequest(new { message = "Invalid license class data." });

            if (_licenseClassService.GetLicenseClassInfoByID(licenseClass.LicenseClassID) == null)
                return NotFound(new { mes = "No license class found with this id" });

            bool success = _licenseClassService.UpdateLicenseClass(licenseClass);

            if (!success)
                return NotFound(new { message = "No Updated try again ." });

            return Ok(new { message = "License class updated successfully." });
        }
    }
}
