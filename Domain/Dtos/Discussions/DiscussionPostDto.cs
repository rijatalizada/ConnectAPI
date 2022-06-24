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
        [Require]
        [StringLength(maximumLength:50)]
        public string Title { get; set; }
        [Require]
        public string Question { get; set; }
        [Require]
        public int CourseId { get; set; }
        [Require]
        public string UserId { get; set; }
    }
}
