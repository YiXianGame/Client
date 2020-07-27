using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Make.MODEL;
using Pack.MODEL;
using Native.Sdk.Cqp;
namespace Pack
{
    public class Transfer_Station
    {
        public static void CQ_To_Server(string msg,long frompersonal, long fromgroup=-1)
        {
            Debug.WriteLine(msg + frompersonal + fromgroup);
            GeneralControl.socket.Request_Client("CQ#"+frompersonal.ToString()+ "|" + fromgroup.ToString() + "|" + msg);
        }
        public static void Send_To_Server(string msg)
        {
            GeneralControl.socket.Request_Client("客户端#"+msg);
        }
        public static void Send_To_CQ(string msg, long frompersonal, long fromgroup = -1)
        {
            CQApi CQApi = GeneralControl.CQApi;
            if (fromgroup == -1)
            {
                CQApi.SendPrivateMessage(frompersonal, msg);
            }
            else
            {
                CQApi.SendGroupMessage(fromgroup, msg);
            }
        }
        public static void Client_Recieve_Server(string msg)
        {
            string[] data = msg.Split('|');
            if (data.Length > 0)
            {

            }
        }
    }
}
