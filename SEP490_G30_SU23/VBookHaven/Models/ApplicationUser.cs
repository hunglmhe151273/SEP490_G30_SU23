using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VBookHaven.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual Customer? Customer { get; set; }
        public virtual Staff? Staff { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
