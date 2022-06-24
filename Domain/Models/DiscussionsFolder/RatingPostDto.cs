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
        [Require]
        [Range(1, 5)]
        public int GivenRating { get; set; }
        [Require]
        public int DiscussionId { get; set; }
        [Require]
        public string UserId{ get; set; }
    }
}
