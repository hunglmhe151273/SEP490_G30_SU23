using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VBookHaven.Models;

namespace VBookHaven.ViewModels
{
    public class ProfileVM
    {
        public ProfileVM()
        {
            StaffProfileVM = new StaffProfileVM();
            ChangePwdVM = new ChangePwdVM();
        }

        public StaffProfileVM StaffProfileVM { get; set; }
        public ChangePwdVM ChangePwdVM { get; set; }
    }

   
    
}
