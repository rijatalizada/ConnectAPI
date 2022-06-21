using Domain.Models.DiscussionsFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public ICollection<Discussion> Discussions  { get; set; }
    }
}
