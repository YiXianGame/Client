using Make.MODEL;
using Native.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pack.BLL
{
    public class Init
    {
        public Init()
        {
            string ip = "127.0.0.1";
            int port = 8888;
            //创建套接字
            Socket_Client s = new Socket_Client(ip, port);
            //连接服务器
            s.Connect_Server();
            //接收服务器的消息
            Thread recv = new Thread(s.Recv_Msg_By_Client);
            recv.Start();
            GeneralControl.socket = s;
        }
        public static void GetCqData(CQApi cQApi,CQLog cQLog)
        {
            GeneralControl.CQApi = cQApi;
            GeneralControl.CQLog = cQLog;
        }
    }
}
