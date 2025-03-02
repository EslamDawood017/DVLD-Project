using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Mapper;
using DVLD_DataAccessLayer.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IPersonService personService;

        public LoginController(IUserService userService , IPersonService personService)
        {
            this.userService = userService;
            this.personService = personService;
        }
        [HttpGet("{UserId}")]
        public IActionResult GetProfile(int UserId)
        {
            var User = userService.GetUserInfoByUserID(UserId);

            if (User == null) 
                return NotFound($"No User with Id : {UserId}");

            var Person = personService.GetPersonById(User.PersonID);

            var ProfileInfo = ProfileMapper.GetProfileInfo(User, Person);

            return Ok(ProfileInfo);
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto model)
        {
            var user = userService.GetUserInfoByUserID(model.UserId);

            if(user == null)
                return NotFound("User Not Found");

            if(user.Password != model.OldPassword)
                return BadRequest("Old password is incorrect.");

            if (!userService.ChangePassword(model.UserId, model.NewPassword))
                return BadRequest("Error on changing password try again later");

            return Ok(new { msg = "Password changed successfully!" });

        }
    }
}
