using Make.MODEL;
using Native.Sdk.Cqp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Pack.BLL
{
    public class Init
    {
        public Init()
        {
            //string ip = "125.124.33.57";
            string ip = "127.0.0.1";
            int port = 28015;
            //创建套接字
            Socket_Client s = new Socket_Client(ip, port);
            //连接服务器
            s.Connect_Server();
            GeneralControl.socket = s;
            Skill_Cards_Load();
            Adventures_Load();
        }
        public void Skill_Cards_Load()
        {
            string filepath = GeneralControl.directory + "\\技能卡";
            DirectoryInfo root = new DirectoryInfo(filepath);
            foreach (FileInfo file in root.GetFiles())
            {
                string json = File.ReadAllText(file.FullName);
                SkillCardsModel skillCardsModel = JsonConvert.DeserializeObject<SkillCardsModel>(json);
                if(skillCardsModel.Cloud=="非云端")skillCardsModel.Add_To_General();
            } 
        }
        public void Adventures_Load()
        {
            string filepath = GeneralControl.directory + "\\奇遇";
            DirectoryInfo root = new DirectoryInfo(filepath);
            foreach (FileInfo file in root.GetFiles())
            {
                string json = File.ReadAllText(file.FullName);
                 Adventure adventure = JsonConvert.DeserializeObject<Adventure>(json);
                if (adventure.Cloud == "非云端") adventure.Add_To_General();
            }
        }
    }
}
