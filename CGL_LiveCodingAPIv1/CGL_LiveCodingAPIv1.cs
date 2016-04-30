using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace CGL
{
    /// <summary>
    /// LiveCodingAPI v1 (ALPHA)
    /// </summary>
    [DataContractAttribute]
    public class CGL_LiveCodingAPIv1 : CGL_LiveCodingAPI_BASE
    {
        public CGL_LiveCodingAPIv1()
        {
            this.BaseApiUri = new Uri("https://www.livecoding.tv/api/v1/");
            this.ClientId = "YOUR_CLIENT_ID";
            this.ClientSecret = "YOUR_SECRET";
            this.CallbackUri = new Uri("http://www.YOURCALLBACK.com");
            this.State = System.Guid.NewGuid();
        }
    }
}
