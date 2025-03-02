using DVLD_BusinessLayer.Helpers;
using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtToken jwt;

        public UserController(IUserService userService , IJwtToken jwt )
        {
            this.userService = userService;
            this.jwt = jwt;
        }
        [HttpGet("GetAll")]
        public IActionResult getAllUsers()
        {
            var users = userService.getAllUsers();

            if (users == null) 
                return NotFound("No Users Was Found");

            return Ok(users);
        }
        [HttpDelete("DeleteUser")]
        public IActionResult deleteUser(int id) 
        {



            if (!userService.IsUserExist(id))
                return NotFound("User Not Found");

            var result = userService.DeleteUser(id);

            if (!result)
                return BadRequest("error , user not deleted try again later");

            return Ok(new {msg = "User Deleted Successfully"});
        }
        [HttpGet("{id}")]
        public IActionResult getUserById(int id)
        {
            if(!userService.IsUserExist(id))
                return NotFound($"No user Found with Id : {id}");

            var user = userService.GetUserInfoByUserID(id);

            if (user == null)
                return BadRequest("Try again later");

            return Ok(user);
        }
        [HttpGet("getUserByPersonId")]
        public IActionResult getUserByPersonIdd(int PersonId)
        {
            if (!userService.IsUserExistForPersonID(PersonId))
                return NotFound($"No user Found with Person Id : {PersonId}");

            var user = userService.GetUserInfoByPersonID(PersonId);

            if (user == null)
                return BadRequest("Try again later");

            return Ok(user);
        }
        [HttpGet("GetUserInfoByUsernameAndPassword")]
        public IActionResult GetUserInfoByUsernameAndPassword(string username, string password)
        {
            if (!userService.IsUserExist(username))
                return NotFound($"no user with username {username}");

            var user = userService.GetUserInfoByUsernameAndPassword(username , password);

            if (user == null)
                return BadRequest(new { msg = "Username or password is invalid" });

            return Ok(user);

        }
        [HttpPost("AddNew")]
        public IActionResult AddnewUser(User newUser)
        {
            if (userService.IsUserExistForPersonID(newUser.PersonID))
                return BadRequest("this person is allready a User");

            if (userService.IsUserExist(newUser.UserName))
                return BadRequest("this user name is allready exist");

            var UserId = userService.AddNewUser(newUser);

            if (UserId == -1)
                return BadRequest("Error Check on your  data and try again");

            return Ok(new { UserId = UserId });
        }
        [HttpPut("Update")]
        public IActionResult UpdateUser([FromBody] User UpdatedUser)
        {
            var User = userService.GetUserInfoByUserID(UpdatedUser.UserID);

            if (User == null)
                return NotFound("User not found.");

            if(userService.IsUserExist(UpdatedUser.UserName) && User.UserID != UpdatedUser.UserID)
                return BadRequest("Username already taken.");

            if (userService.IsUserExistForPersonID(UpdatedUser.PersonID) && User.PersonID != UpdatedUser.PersonID)
                return BadRequest("Person is already assigned to another user.");


            var result = userService.UpdateUser(UpdatedUser);
            if (!result)
                return BadRequest("user not updated try again later");

            return Ok(new {msg = "User Updated successfully"});
        }
        [HttpGet("CheckUsernameExists/{username}")]
        public IActionResult CheckUsernameExists(string username)
        {
            bool exists = userService.IsUserExist(username);
            return Ok(new { exists });
        }
        [HttpGet("CheckPersonAssigned/{personId}")]
        public IActionResult CheckPersonAssigned(int personId)
        {
            bool assigned = userService.IsUserExistForPersonID(personId);
            return Ok(new { assigned });
        }
        [HttpPost("login")]
        public IActionResult Login(loginDto loginDto)
        {
            var user = userService.GetUserInfoByUsernameAndPassword(loginDto.UserName , loginDto.Password);

            if (user == null) 
                return Unauthorized(new { message = "Invalid username or password" });
           

            var token = jwt.GenerateJwtToken(user);

            return Ok(new { token, UserId = user.UserID , userName = user.UserName, role = user.Role  });
        }
    }
}
