using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhonebookWebApplication.Dtos;
using PhonebookWebApplication.Service;

namespace PhonebookWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        [HttpPost("SaveUser")]
        public ActionResult<UserResponse> SaveUser(UserRequest request)
        {
            var result = _userRepository.SaveUser(request);
            return result;
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<GetUserDetailsResponse> GetAllUsers()
        {
            var response=_userRepository.GetAllUsers();
            if (response.Users.Count == 0)
                return NotFound(response);
            return Ok(response);    
        }

        [HttpPut("UpdateUser")]
        public ActionResult<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var res = _userRepository.UpdateUser(request);
            if (res.Id<=0)
            {
                return NotFound(res);
            }

            return Ok(res);

        }

        [HttpGet("{name}")]
        public ActionResult<GetUserDetailsResponse> GetUserByName(string name)
        {
            var res= _userRepository.GetUserByName(name);
            if (res.Users.Count==0)
            {
                return NotFound(res);
            }

            return Ok(res);
            
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<GetUserDetailsResponse> GetUserById(int id)
        {
            var res = _userRepository.GetUserId(id);
            if (res.User == null)
            {
                return NotFound(res);
            }

            return Ok(res);

        }

        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<DeleteResponse> DeleteUser(int id) 
        {
            var res=_userRepository.DeleteUser(id);
            if (res.Id<=0)
            {
                return NotFound(res);
            }

            return Ok(res);

        }
    }
}
