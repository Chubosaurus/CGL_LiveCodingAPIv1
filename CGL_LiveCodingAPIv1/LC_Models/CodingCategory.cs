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
    /// A Coding Category object.
    /// </summary>
    public class CodingCategory
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
    }

    /// <summary>
    /// Alias for CodingCategory to match other Response Types.
    /// </summary>
    public class CodingCategory_Response : CodingCategory
    {
    }

    /// <summary>
    /// Response to GET of all coding categories.
    /// </summary>
    public class CodingCategoryList_Response
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<CodingCategory> Results { get; set; }

        [JsonIgnore]
        public List<CodingCategory> CodingCategories
        {
            get { return this.Results; }
        }
    } 
}
