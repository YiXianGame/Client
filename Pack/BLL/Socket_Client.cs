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
        static MessageHandle messageHandle = new MessageHandle();
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
            client_socket.BeginReceive(messageHandle.DataBuffer, messageHandle.ContentSize, messageHandle.remainSize, SocketFlags.None, ReceiveCallBack,client_socket);
        }
            
        /// <summary>
        /// 接收来自服务器的消息
        /// </summary>
        public void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);    //接收到的数据量
                if (count == 0) ///说明客户端已经已经断开连接了
                {
                    if (clientSocket != null)
                    {
                        clientSocket.Close();
                    }
                    return;
                }
                //j解析数据（把新接收的数据传入）
                messageHandle.ReadMessage(count);
                //开始异步接收数据
                clientSocket.BeginReceive(messageHandle.DataBuffer, messageHandle.ContentSize, messageHandle.remainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception e)///说明客户端已经已经断开连接了,异常断开
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
                            Console.WriteLine("连接失败，10秒后重新尝试");
                            Thread.Sleep(10000);
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
        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        public void Request_Client(string send_msg)
        {
            if (client_socket.Connected)
            {
                client_socket.Send(messageHandle.Convert_SendMsg(send_msg));
            }
            else Console.WriteLine("服务端未连接,消息发送失败:"+send_msg);
        }
        public class MessageHandle
        {
            //表头的数据长度为4个个字节，表示后面的数据的长度
            //保证能够每次接收发送的消息小于1024bit大小，否则无法完整接收整条   数据
            private byte[] dataBuffer = new byte[20480];
            //从dataBuffer已经存了多少个字节数据
            private int contentSize = 0;

            public int ContentSize
            {
                get { return contentSize; }
            }
            /// <summary>
            /// 剩余多少存储空间
            /// </summary>
            public int remainSize
            {
                get { return dataBuffer.Length - contentSize; }
            }

            public byte[] DataBuffer
            {
                get { return dataBuffer; }
            }

            /// <summary>
            /// 解析数据 ,count 新读取到的数据长度
            /// </summary>
            public void ReadMessage(int count)
            {
                contentSize += count;
                //用while表示缓存区，可能有多条数据
                while (true)
                {
                    //缓存区小于4个字节，表示连表头都无法解析
                    if (contentSize <= 4) return;
                    //读取四个字节数据，代表这条数据的内容长度（不包括表头的4个数据）
                    int receiveCount = BitConverter.ToInt32(dataBuffer, 0);
                    //缓存区中的数据，不够解析一条完整的数据
                    if (contentSize - 4 < receiveCount) return;

                    //2、解析数据
                    //从除去表头4个字节开始解析内容，解析的数据长度为（表头数据表示的长度）
                    string receiveStr = Encoding.UTF8.GetString(dataBuffer, 4, receiveCount);
                    Console.WriteLine(receiveStr);
                    //把剩余的数据Copy到缓存区头部位置
                    Array.Copy(dataBuffer, 4 + receiveCount, dataBuffer, 0, contentSize - 4 - receiveCount);
                    contentSize = contentSize - 4 - receiveCount;
                    Receive_Msg(receiveStr);
                }
            }
            public static void Receive_Msg(string receiveStr)
            {
                string[] msg = receiveStr.Split('$');
                //数据处理
                if (msg.Length > 0)
                {
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
            /// <summary>
            /// 构造发送数据
            /// </summary>
            /// <param name="msg"></param>
            /// <returns></returns>
            public byte[] Convert_SendMsg(string msg)
            {
                //构造内容
                byte[] bodyBytes = Encoding.UTF8.GetBytes(msg);
                //构造表头数据，固定4个字节的长度，表示内容的长度
                byte[] headerBytes = BitConverter.GetBytes(bodyBytes.Length);
                byte[] tempBytes = new byte[headerBytes.Length + bodyBytes.Length];
                ///拷贝到同一个byte[]数组中，发送出去..
                Buffer.BlockCopy(headerBytes, 0, tempBytes, 0, headerBytes.Length);
                Buffer.BlockCopy(bodyBytes, 0, tempBytes, headerBytes.Length, bodyBytes.Length);
                return tempBytes;
            }

        }
    }
}
