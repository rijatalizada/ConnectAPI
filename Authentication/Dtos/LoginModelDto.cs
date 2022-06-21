using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Dtos
{
    public class LoginModelDto
    {
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
