
using Make.MODEL;
using Native.Sdk.Cqp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pack.BLL
{
    public class XY
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
            if (fromgroup == -1)
            {
                GeneralControl.CQApi.SendPrivateMessage(frompersonal, msg);
            }
            else
            {
                GeneralControl.CQApi.SendGroupMessage(fromgroup, msg);
            }
        }
        public static void Client_Recieve_Server(string msg)
        {
            string[] data = msg.Split('#');
            MessageBox.Show(msg);
            if (data.Length > 0)
            {
                if(data.Length==2 && data[0] == "用户")
                {
                    if (data[1] != null)
                    {
                        MessageBox.Show(data[1]);
                        GeneralControl.Menu_Person_Informations_Class.Instance.Author = JsonConvert.DeserializeObject<Author>(data[1]);
                    }
                }
            }
        }
    }
}
