using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft;
using Newtonsoft.Json;

namespace CGL.LC_Models
{
    /// <summary>
    /// A Video object.
    /// </summary>
    public class Video
    {
        public string Url { get; set; }
        public string Slug { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonProperty("coding_category")]
        public string CodingCategory { get; set; }
        public string Difficulty { get; set; }
        public string Language { get; set; }

        // TODO(duan): create an ENUM of ['game' or 'app' or 'website' or 'codetalk' or 'other']
        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("creation_time")]
        public string CreationTime { get; set; }

        public int Duration { get; set; }

        // TODO(duan): create an ENUM of  ['us-stlouis' or 'eu-london']
        public string Region { get; set; }

        [JsonProperty("viewers_overall")]
        public int ViewersOverall { get; set; }

        [JsonProperty("viewing_urls")]
        public List<string> ViewingUrls { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
    }

    /// <summary>
    /// Alias for Video to match other Response Types.
    /// </summary>
    public class Video_Response : Video
    {
    }

    /// <summary>
    /// Response to GET of various Video related functions that returns a List of Video(s).
    /// </summary>
    public class VideoList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Video> Results { get; set; }

        [JsonIgnore]
        public List<Video> Videos
        {
            get { return Results; }
        }
    }

    /// <summary>
    /// Response to GET of the Latest User's videos.
    /// </summary>
    public class LatestVideoList_Response : List<Video>
    {
    }
}
