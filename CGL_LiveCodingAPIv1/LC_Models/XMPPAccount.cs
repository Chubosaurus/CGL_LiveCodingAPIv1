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
    /// XMPP account information.
    /// </summary>
    public class XMPPAccount
    {
        public string User { get; set; }
        public string JId { get; set; }
        public string Password { get; set; }

        // TODO(duan): Convert to UI.Color
        public string Color { get; set; }
        [JsonProperty("is_staff")]
        public bool IsStaff { get; set; }
    }

    /// <summary>
    /// Alias for XMPPAccount to match other Response Types.
    /// </summary>
    public class XMPPAccount_Response : XMPPAccount
    {
    }
}
