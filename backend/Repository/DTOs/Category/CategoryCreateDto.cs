using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = null!;
    }

}
