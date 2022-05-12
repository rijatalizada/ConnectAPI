using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.HomePage
{
    public class HeaderDto
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public byte? Order { get; set; }
    }
}
