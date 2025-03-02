using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestTypeController : ControllerBase
    {
        private readonly ITestTypeService _TestTypeService;

        public TestTypeController(ITestTypeService TestTypeService)
        {
            _TestTypeService = TestTypeService;
        }
        [HttpGet("getAll")]
        public IActionResult GetAllTestType()
        {
            var testTypes = _TestTypeService.GetAllTestTypes();

            if (!testTypes.Any())
                return NotFound("No Test Type Found in the system");

            return Ok(testTypes);
        }
        [HttpGet("{id}")]
        public IActionResult GetTestTypeById([FromRoute] int id)
        {
            var testType = _TestTypeService.GetTestTypeInfoById(id);

            if (testType == null)
                NotFound($"No Test Type with Id : {id}");

            return Ok(testType);
        }
        [HttpPost("addNew")]
        public IActionResult AddNewTestType([FromBody] TestType testType)
        {
            var AppTypeId = _TestTypeService.AddNewTestType(testType);

            if (AppTypeId != -1)
                return Ok(AppTypeId);

            return BadRequest("Enter valid Data");
        }
        [HttpPut("Update")]
        public IActionResult UpdateTestType(TestType testType)
        {
            var TestType = _TestTypeService.GetTestTypeInfoById(testType.TestTypeID);

            if (TestType == null)
                return NotFound($"No Test Type with Id : {testType.TestTypeID}");

            var result = _TestTypeService.UpdateTestType(testType);

            if (!result)
                return BadRequest("enter a valid data");

            return Ok(new { update = result });

        }
    }
}
