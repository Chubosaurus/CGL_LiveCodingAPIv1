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
    /// The ViewingKey of specific User.
    /// </summary>
    public class ViewingKey
    {
        public string Url { get; set; }
        [JsonProperty("viewing_key")]
        public string VKey { get; set; }
    }

    /// <summary>
    /// Alias for ViewingKey to match other Response Types.
    /// </summary>
    public class ViewKey_Response : ViewingKey
    {
    }
}
