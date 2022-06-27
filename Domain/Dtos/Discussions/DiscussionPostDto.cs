using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Discussions
{
    public class DiscussionPostDto
    {
        [Required]
        [StringLength(maximumLength:50)]
        public string Title { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
