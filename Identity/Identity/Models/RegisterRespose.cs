using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class RegisterRespose
    {
        public IdentityResult Result { get; set; } = null!;
        public int Userid { get; set; }
    }
}
