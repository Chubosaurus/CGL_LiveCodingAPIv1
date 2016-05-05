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
    /// A LiveStream object.
    /// </summary>
    public class LiveStream
    {
        public string Url { get; set; }
        public string User { get; set; }

        [JsonProperty("user__slug")]
        public string UserSlug { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [JsonProperty("coding_category")]
        public string CodingCategory { get; set; }
        public string Difficulty { get; set; }
        public string Language { get; set; }
        public string Tags { get; set; }

        [JsonProperty("is_live")]
        public bool IsLive { get; set; }

        [JsonProperty("viewers_live")]
        public int ViewersLive { get; set; }

        [JsonProperty("viewing_urls")]
        public List<string> ViewingUrls { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
    }

    /// <summary>
    /// Alias for LiveStream to match other Response Types.
    /// </summary>
    public class LiveStream_Response : LiveStream
    {
    }

    /// <summary>
    /// A LiveStream object with extra authenticated properties.
    /// </summary>
    public class AuthenticatedLiveStream : LiveStream
    {
        [JsonProperty("streaming_key")]
        public string StreamingKey { get; set; }
        [JsonProperty("streaming_url")]
        public string StreamingUrl { get; set; }
    }

    /// <summary>
    /// Alias for AuthenticatedLiveStream to match other Response Types.
    /// </summary>
    public class AuthenticatedLiveStream_Response : AuthenticatedLiveStream
    {
    }

    /// <summary>
    /// A Response to GET of various LiveStream functions that return a List of LiveStream(s).
    /// </summary>
    public class LiveStreamList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public List<LiveStream> Results { get; set; }

        [JsonIgnore]
        public List<LiveStream> LiveStreams
        {
            get { return Results; }
        }
    }

    /// <summary>
    /// A Response to GET of various AuthenticatedLiveStream functions that return a List of AuthenticatedLiveStream(s).
    /// </summary>
    public class AuthenticatedLiveStreamList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public List<AuthenticatedLiveStream> Results { get; set; }

        [JsonIgnore]
        public List<AuthenticatedLiveStream> LiveStreams
        {
            get { return Results; }
        }
    }
}
