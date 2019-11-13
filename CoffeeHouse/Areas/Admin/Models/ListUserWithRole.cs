using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Areas.Admin.Models
{
    public class ListUserWithRole
    {
        public IdentityUser User { get; set; }
        public string RoleName { get; set; }
    }
}
