using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interface.Repositories
{
    public interface IUserRepository
    {      
        Task <RegisterRespose> RegisterAsync(RegisterModel model);
    }
}
