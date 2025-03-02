using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        
        [HttpGet("{driverId}")]
        public IActionResult GetDriver(int driverId)
        {
            var driver = _driverService.GetDriverInfoByDriverID(driverId);

            if (driver == null)
                return NotFound(new { message = "Driver not found." });

            return Ok(driver);
        }
        [HttpGet("all")]
        public IActionResult GetAllDrivers()
        {
            var drivers = _driverService.GetAllDrivers();
            return Ok(drivers);
        }

        [HttpPost("add")]
        public IActionResult AddDriver(int personId , int CreatedByUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid driver data." });

            int success = _driverService.AddNewDriver(personId, CreatedByUserId);

            if (success > 0 )
                return Ok(new { message = "Driver added successfully." });


            return StatusCode(500, new { message = "Failed to add driver." });      
        }

        
        [HttpPut("update/{driverId}")]
        public IActionResult UpdateDriver(int driverId, int personId , int CreatedByUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid driver data : " + ModelState.Values});

            bool success = _driverService.UpdateDriver(driverId, personId , CreatedByUserId);

            if (!success)
                return NotFound(new { message = "Driver not found." });

            return Ok(new { message = "Driver updated successfully." });
        }

        
    }
}
