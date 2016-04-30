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
    /// A Language object.
    /// </summary>
    public class Language
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Alias for Language to match other Response Types.
    /// </summary>
    public class Language_Response : Language
    {       
    }

    /// <summary>
    /// A Response to GET of various Language functions that return a List of Language(s).
    /// </summary>
    public class LanguageList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public List<Language> Results { get; set; }

        [JsonIgnore]
        public List<Language> Languages
        {
            get { return Results; }
        }
    }    
}
