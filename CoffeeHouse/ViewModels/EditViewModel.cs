using CoffeeHouse.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public RoleType Type { get; set; }
        public List<SelectListItem> RoleTypes { get; set; }
        public EditViewModel()
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
