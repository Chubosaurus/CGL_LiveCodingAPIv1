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
    /// A Scheduled Broadcast object.
    /// </summary>
    public class ScheduledBroadcast
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LiveStream { get; set; }

        [JsonProperty("coding_category")]
        public string CodingCategory { get; set; }

        public string Difficulty { get; set; }

        // NOTE(duan): trap this values to see what they really mean
        // TODO(duan): Convert to DateTime class
        public string Start_Time { get; set; }
        public string Start_Time_Original_TimeZone { get; set; }
        public string Original_TimeZone { get; set; }

        [JsonProperty("is_featured")]
        public bool IsFeatured { get; set; }

        [JsonProperty("is_recurring")]
        public bool IsRecurring { get; set; }
    }

    /// <summary>
    /// Alias for ScheduledBroadcast to match other Response Types.
    /// </summary>
    public class ScheduledBroadcast_Response : ScheduledBroadcast
    {
    }

    /// <summary>
    /// A Response to GET of various ScheduledBroadcast functions that return a List of ScheduledBroadcast(s).
    /// </summary>
    public class ScheduledBroadcastList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public List<ScheduledBroadcast> Results { get; set; }

        [JsonIgnore]
        public List<ScheduledBroadcast> ScheduledBroadcasts
        {
            get { return Results; }
        }
    }
}
