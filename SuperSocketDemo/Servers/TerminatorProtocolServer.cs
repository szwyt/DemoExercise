using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

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