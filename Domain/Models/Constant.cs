using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Constant : BaseEntity
    {
        public int Id { get; set; }
        public string LogoURL { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string RedditUrl { get; set; }
    }
}
