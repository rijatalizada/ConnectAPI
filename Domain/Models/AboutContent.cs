using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AboutContent : BaseEntity
    {
        public int Id { get; set; }
        public string HeadingText { get; set; }
    }
}
