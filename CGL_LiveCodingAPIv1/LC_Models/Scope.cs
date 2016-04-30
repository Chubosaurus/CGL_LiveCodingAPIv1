using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGL.LC_Models
{
    /// <summary>
    /// LiveCoding Scope Permissions
    /// </summary>
    // NOTE(duan): we are going to do this manually rather then use the [Flags]
    public enum Scope
    {
        None = 0,
        Read = 1,
        Read_Viewer = 2,
        Read_User = 4,
        Read_Channel = 8,
        Chat = 16,
        FullControl = 0x1F
    }
}
