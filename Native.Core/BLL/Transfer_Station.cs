using Native.Core.Domain;
using Native.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Core.BLL
{
    public class Transfer_Station
    {
        public static void CQ_To_Server(string msg,long frompersonal, long fromgroup=-1)
        {
            Debug.WriteLine(msg + frompersonal + fromgroup);
            Generol.socket.Request_Client("CQ#"+frompersonal.ToString()+ "|" + fromgroup.ToString() + "|" + msg);
        }
        public static void Send_To_Server(string msg)
        {
            Generol.socket.Request_Client("客户端#"+msg);
        }
        public static void Send_To_CQ(string msg, long frompersonal, long fromgroup = -1)
        {
            CQApi CQApi = AppData.CQApi;
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
