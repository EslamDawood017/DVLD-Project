using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet("getAll")]
        public IActionResult GetAllPeople()
        {
            var people = _personService.GetAllPeople();

            if (people == null) 
                return NotFound("no people was found");

            return Ok(people);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPersonById(int id)
        {
            Person person = _personService.GetPersonById(id);

            if (person == null)
                return NotFound("Person Not Found");

            return Ok(person);
        }
        [HttpGet("nationalNo")]     
        public IActionResult GetPersonByNationalNo(string NationalNo)
        {
            Person person = _personService.GetPersonByNationalNo(NationalNo);

            if (person == null)
                return NotFound("Person Not Found");

            return Ok(person);
        }
        [HttpPost("AddNew")]
        public IActionResult AddNewPerson([FromBody] Person person) 
        { 
            int personId = _personService.AddNewPerson(person);

            if (personId == -1)
                return BadRequest();

            return Ok(personId);
        }
        [HttpPut("Update")]
        public IActionResult UpdatePerson([FromBody] Person UpdatedPerson) 
        { 
            var person = _personService.GetPersonById(UpdatedPerson.PersonID);

            if (person == null)
                return NotFound("Person Not Found");

            if (_personService.UpdatePerson(UpdatedPerson))
                return Ok(new { meg = "Person Updated SuccessFully" });
            else
                return BadRequest("UnValid Data");
        }
        [HttpDelete]
        public IActionResult DeletePerson(int personId) 
        {
            var person =_personService.GetPersonById(personId);

            if (person == null)
                return NotFound("Person Not Found");

            if (_personService.DeletePerson(personId))
                return Ok(new { msg = "Person Deleted successfully" });
            else
                return BadRequest("User Can't be Deleted");

        }
        [HttpGet("isPersonExistById")]
        public IActionResult IsPersonExist(int personId) 
        {
            if (_personService.isPersonExistById(personId))
                return Ok("Person is Exist");
            else
                return NotFound("Person Not Found"); 
        }
        [HttpGet("isPersonExistByNationalNo")]
        public IActionResult IsPersonExist(string NationalNo)
        {
            if (_personService.isPersonExistByNationalNo(NationalNo))
                return Ok("Person is Exist");
            else
                return NotFound("Person Not Found");
        }

    }
}
