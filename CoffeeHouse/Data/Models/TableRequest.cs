using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Data.Models
{
    public class TableRequest
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Time { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Note { get; set; }
        public string Status { get; set; }

        public TableRequest()
        {
            Status = "Đang chờ";
        }
    }
}
