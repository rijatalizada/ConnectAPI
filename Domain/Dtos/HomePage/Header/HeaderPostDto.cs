using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.HomePage.Header
{
    public class HeaderPostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        public byte? Order { get; set; }
    }
}
