using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Data.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        [Display(Name = "Tên sản phẩm")]
        public string DrinkName { get; set; }

        [Required]
        [Range(0, 999.99)]
        [Display(Name = "Giá tiền")]
        public double Price { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Mô tả chi tiết")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Photo { get; set; }

        [Required]
        [Display(Name = "Loại sản phẩm")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
