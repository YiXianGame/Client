using Make.MODEL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack.BLL
{
    public class Socket_Client
    {
        public Socket_Client(string ip, int port)
        {
            this.iPAddress = IPAddress.Parse(ip);
            this.port = port;
        }

        /// <summary>
        /// 套接字
        /// </summary>
        private Socket client_socket;
        /// <summary>
        /// 客户端要连接的ip地址
        /// </summary>
        private IPAddress iPAddress;
        /// <summary>
        /// 客户端要连接的端口好
        /// </summary>
        private int port;
        /// <summary>
        /// 创建客户端连接的套接字
        /// </summary>
        /// <returns></returns>
        public Socket Create_Client_Socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect_Server()
        {
            if (client_socket!=null && client_socket.Connected)
            {
                client_socket.Close();
            }
            client_socket = Create_Client_Socket();
            //tcp连接服务器的时候只需要连接一次，因为tcp是长链接
            client_socket.Connect(new IPEndPoint(iPAddress, port));
        }

        /// <summary>
        /// 接收来自服务器的消息
        /// </summary>
        public void Recv_Msg_By_Client()
        {   
            while (true)
            {
                try
                {
                    byte[] ser_msg = new byte[20480];
                    int count = client_socket.Receive(ser_msg);
                    string origin_msg = Encoding.UTF8.GetString(ser_msg, 0, count);
                    if (count > 0) Console.WriteLine("总计字节:" + count + " 内容:" + origin_msg);
                    else throw new Exception("接收字节为0");
                    foreach(string str_msg in origin_msg.Split('&'))
                    {
                        //GeneralControl.CQLog.Debug("服务端", str_msg);
                        if (count > 0)
                        {
                            string[] msg = str_msg.Split('$');
                            if (msg.Length >= 2)
                            {
                                if (msg[0] == "客户端") XY.Client_Recieve_Server(msg[1]);
                                else if (msg[0] == "CQ")
                                {
                                    string[] cq_msg = msg[1].Split('|');
                                    if (cq_msg.Length == 4)
                                    {
                                        if (cq_msg[0] == "Common")
                                        {
                                            XY.Send_To_CQ(cq_msg[3], long.Parse(cq_msg[1]), long.Parse(cq_msg[2]));
                                        }
                                        else if (cq_msg[0] == "XML")
                                        {
                                            XY.Send_To_CQ_XML(cq_msg[3], long.Parse(cq_msg[1]), long.Parse(cq_msg[2]));
                                        }
                                        else if (cq_msg[0] == "JSON")
                                        {
                                            XY.Send_To_CQ_JSON(cq_msg[3], long.Parse(cq_msg[1]), long.Parse(cq_msg[2]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (!client_socket.Connected)
                    {
                        Console.WriteLine("正在尝试重连");
                        int cnt = 0;
                        while (true)
                        {
                            Console.WriteLine("第" + ++cnt + "次尝试");
                            try { Connect_Server(); }
                            catch (Exception)
                            {
                                Console.WriteLine("连接失败，5秒后重新尝试");
                                Thread.Sleep(5000);
                                continue;
                            }
                            if (client_socket.Connected)
                            {
                                Console.WriteLine("连接成功！");
                                break;
                            }
                        }
                    }
                    else Console.WriteLine(e.Message);
                }
            }
        }
        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        public void Request_Client(string send_msg)
        {
            byte[] by_msg=null;
            by_msg = Encoding.UTF8.GetBytes(send_msg + "&");
            client_socket.Send(by_msg);
        }
    }
}
