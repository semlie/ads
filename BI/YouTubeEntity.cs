using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class YouTubeEntity
    {
        public List<YouTubeReasults> Videos { get; set; }
        public List<YouTubeReasults> Channels { get; set; }
        public List<YouTubeReasults> Playlists { get; set; }
        public string Query { get; set; }

    }
    public class YouTubeReasults
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }

      
    }

}
