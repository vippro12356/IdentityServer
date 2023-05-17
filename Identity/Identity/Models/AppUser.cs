using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; } = null!;
    }
}
