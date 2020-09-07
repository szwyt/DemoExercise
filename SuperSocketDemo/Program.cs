using System;
using System.Linq;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 注意是TelnetServer
            var appServer = new TelnetServer();
            appServer.Setup("192.168.0.20",300);
            // 开始监听
            appServer.Start();
            //1.
            appServer.NewSessionConnected += new SessionHandler<TelnetSession>(appServer_NewSessionConnected);
            appServer.SessionClosed += appServer_NewSessionClosed;
            appServer.NewRequestReceived += appServer_NewRequestReceived;
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
            // 停止服务器。
            appServer.Stop();
        }

        //1.
        static void appServer_NewSessionConnected(TelnetSession session)
        {
            //Console.WriteLine($"服务端得到来自客户端的连接成功");
            //var count = appServer.GetAllSessions().Count();
            //Console.WriteLine("~~" + count);
            //session.Send("Welcome to SuperSocket Telnet Server");
        }

        static void appServer_NewSessionClosed(TelnetSession session, CloseReason aaa)
        {
            //Console.WriteLine($"服务端 失去 来自客户端的连接" + session.SessionID + aaa.ToString());
            //var count = appServer.GetAllSessions().Count();
            //Console.WriteLine(count);
        }

        //2.
        static void appServer_NewRequestReceived(TelnetSession session, StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key.ToUpper())
            {
                case ("ECHO"):
                    session.Send(requestInfo.Body);
                    break;

                case ("ADD"):
                    session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
                    break;

                case ("MULT"):

                    var result = 1;

                    foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
                    {
                        result *= factor;
                    }

                    session.Send(result.ToString());
                    break;
            }
        }

    }
    
}
