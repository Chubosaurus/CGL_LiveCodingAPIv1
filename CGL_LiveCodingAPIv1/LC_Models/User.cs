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
    /// An User object.
    /// </summary>
    public class User
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Slug { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonProperty("favorite_programming")]
        public string FavoriteProgramming { get; set; }
        [JsonProperty("favorite_ide")]
        public string Favorite_IDE { get; set; }
        [JsonProperty("favorite_coding_background_music")]
        public string FavoriteCodingBackgroundMusic { get; set; }
        [JsonProperty("favorite_code")]
        public string FavoriteCode { get; set; }
        [JsonProperty("years_programming")]
        public int YearsProgramming { get; set; }
        [JsonProperty("want_learn")]
        public List<string> WantLearn { get; set; }
        [JsonProperty("registration_date")]
        public string RegistrationDate { get; set; }
        public string TimeZone { get; set; }
    }

    /// <summary>
    /// Alias for User to match other Response Types.
    /// </summary>
    public class User_Response : User
    {
    }

    /// <summary>
    /// Response to GET of various User related functions that return a List of User(s).
    /// </summary>
    public class UserList_Response : List<User>
    {
    }
}
