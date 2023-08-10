using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VBookHaven.Models;

namespace VBookHaven.ViewModels
{
    public class UpdateStaffVM
    {
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        // Staff_info
        public ApplicationUser? ApplicationUser { get; set; }
        [Display(Name = "Ảnh Đại Diện")]
        [DataType(DataType.Upload)]
        public IFormFile? Staff_ImageFile { get; set; }
    }
}
