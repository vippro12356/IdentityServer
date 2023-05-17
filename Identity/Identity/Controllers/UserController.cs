using Identity.Interface.Repositories;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm]RegisterModel model) 
        { 
            var response = await _userRepository.RegisterAsync(model);
            if(response.Result.Succeeded)
            {
                return Ok(response.Userid);
            }
            return BadRequest(response.Result.Errors);
        }
    }
}
