using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DiscussionsFolder
{
    public class RatingPostDto
    {
        [Required]
        [Range(1, 5)]
        public int GivenRating { get; set; }
        [Required]
        public int DiscussionId { get; set; }
        [Required]
        public string UserId{ get; set; }
    }
}
