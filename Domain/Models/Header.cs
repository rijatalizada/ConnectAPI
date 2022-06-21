using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Header : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Image { get; set; }
        public byte? Order { get; set; }
    }
}
