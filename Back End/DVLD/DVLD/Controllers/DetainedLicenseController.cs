using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DetainedLicenseController : ControllerBase
    {
        private readonly IDetainedLicenseService _detainedLicenseService;
        private readonly ILicenseService licenseService;

        public DetainedLicenseController(IDetainedLicenseService detainedLicenseService ,
            ILicenseService licenseService)
        {
            _detainedLicenseService = detainedLicenseService;
            this.licenseService = licenseService;
        }

        [HttpGet("all")]
        public IActionResult GetAllDetainedLicenses()
        {
            var licenses = _detainedLicenseService.GetAllDetainedLicenses();

            return Ok(licenses);
        }
        [HttpGet("{detainId}")]
        public IActionResult GetDetainedLicenseById(int detainId)
        {
            var license = _detainedLicenseService.GetDetainedLicenseInfoByID(detainId);

            if (license == null)
                return NotFound(new { message = "Detained license not found." });

            return Ok(license);
        }
        [HttpGet("license/{licenseID}")]
        public IActionResult GetDetainedLicenseByLicenseId(int licenseID)
        {
            var license = _detainedLicenseService.GetDetainedLicenseInfoByLicenseID(licenseID);
            if (license == null)
                return NotFound(new { message = "No detained license found for this License ID." });

            return Ok(license);
        }
        [HttpGet("isDetained/{licenseID}")]
        public IActionResult IsLicenseDetained(int licenseID)
        {
            bool isDetained = _detainedLicenseService.IsLicenseDetained(licenseID);

            return Ok(new { isDetained });
        }
        [HttpPost("add")]
        public IActionResult AddDetainedLicense([FromBody] NewDetainDto request)
        {
            if (request == null)
                return BadRequest(new { message = "Invalid detained license data." });

            int newDetainId = _detainedLicenseService.AddNewDetainedLicense(request);

            return CreatedAtAction(nameof(GetDetainedLicenseById), new { detainId = newDetainId },
                new { message = "Detained license added successfully.", detainId = newDetainId });
        }
        [HttpPut("release/{LicenseID}")]
        public IActionResult ReleaseDetainedLicense(int LicenseID, int ReleasedByUserId)    
        {
            if (licenseService.GetLicecnseInfoById(LicenseID) == null)
                return BadRequest(new { message = "License is Not exist." });

            if(!_detainedLicenseService.IsLicenseDetained(LicenseID))
                return BadRequest(new { message = "This License is not detained or released." });


            bool success = _detainedLicenseService.ReleaseDetainedLicense(LicenseID , ReleasedByUserId);

            if (!success)
                return NotFound(new { message = "Detained license not found or already released." });

            return Ok(new { message = "Detained license released successfully." });
        }
        [HttpPut("update")]
        public IActionResult UpdateDetainedLicense([FromBody] NewDetainDto request)
        {
            if (request == null)
                return BadRequest(new { message = "Invalid detained license data." });

            bool success = _detainedLicenseService.UpdateDetainedLicense(request);

            if (!success)
                return NotFound(new { message = "Detained license not found." });

            return Ok(new { message = "Detained license updated successfully." });
        }
    }
}
