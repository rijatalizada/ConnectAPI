using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Discussions.Replies
{
    public class ReplyPostDto
    {
        [Required]
        public string Reply { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int DiscussionId { get; set; }
    }
}
