using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace VBookHaven.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual Customer? Customer { get; set; }
        public virtual Staff? Staff { get; set; }
        [NotMapped]
        [ValidateNever]
        public string Role { get; set; }
    }
}
