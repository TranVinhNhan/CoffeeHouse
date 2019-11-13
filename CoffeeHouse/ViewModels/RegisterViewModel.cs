using CoffeeHouse.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(10, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 10)]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public RoleType Type { get; set; }
        public List<SelectListItem> RoleTypes { get; set; }
        public RegisterViewModel()
        {
            RoleTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = ((int)RoleType.Admin).ToString(),
                    Text = RoleType.Admin.ToString()
                },
                new SelectListItem
                {
                    Value = ((int)RoleType.User).ToString(),
                    Text = RoleType.User.ToString()
                },
                new SelectListItem
                {
                    Value = ((int)RoleType.Staff).ToString(),
                    Text = RoleType.Staff.ToString()
                }
            };
        }
    }
}
