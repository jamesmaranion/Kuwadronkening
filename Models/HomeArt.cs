using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuwadro.Models
{
    public class HomeArt
    {
        public List<Art> FeaturedArt { get; set; }
        public List<Art> RecommendedArt { get; set; }
        public List<Art> PopularArt { get; set; }
        public List<ApplicationUser> DiscoverArtist { get; set; }
    }
}
