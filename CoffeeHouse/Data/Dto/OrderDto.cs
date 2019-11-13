using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Data.Dto
{
    public class OrderDto
    {
        [StringLength(255)]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Address Line")]
        [Required(ErrorMessage = "Address Line is required")]
        public string AddressLine { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [StringLength(6)]
        [Required(ErrorMessage = "Zipcode is required")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Number only")]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
