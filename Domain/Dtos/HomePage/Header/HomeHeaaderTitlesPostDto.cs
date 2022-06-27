using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.HomePage.Header
{
    public class HomeHeaaderTitlesPostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
    }
}
