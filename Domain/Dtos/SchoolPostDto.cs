using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SchoolPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Fullname { get; set; }
    }
}
