using Identity.Interface.Repositories;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;              
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;                 
        }
        public async Task<RegisterRespose> RegisterAsync(RegisterModel model)
        {
            var user = new AppUser()
            {
                Name=model.Name,
                UserName = model.Name,
                Email=model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var response = new RegisterRespose();
            if (!result.Succeeded)//create failed
            {
                response.Result = result;
            }
            response.Result = result; //get response after create user
            response.Userid = user.Id;//get user id after create   
            return response;
        }
    }
}
