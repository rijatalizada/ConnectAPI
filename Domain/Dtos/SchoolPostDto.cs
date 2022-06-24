using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SchoolPostDto
    {
        [Require]
        public string Name { get; set; }
        [Require]
        public string Fullname { get; set; }
    }
}
