using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DiscussionsFolder
{
    public class Rating : BaseEntity
    {
        public int Id { get; set; }
        [Range(1,5)]
        public int GivenRating { get; set; }
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
