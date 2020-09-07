using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketDemo.Servers
{
    /// <summary>
    /// TerminatorProtocolServer
    /// Each request end with the terminator "##"
    /// ECHO Your message##
    /// </summary>
    public class TerminatorProtocolServer : AppServer
    {
        public TerminatorProtocolServer()
            : base(new TerminatorReceiveFilterFactory("##"))
        {

        }
    }
}
