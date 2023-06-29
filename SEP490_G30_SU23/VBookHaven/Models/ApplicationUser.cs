using Microsoft.AspNetCore.Identity;

namespace VBookHaven.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual Customer? Customer { get; set; }
        public virtual Staff? Staff { get; set; }
    }
}
