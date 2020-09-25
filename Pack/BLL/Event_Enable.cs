using Make.MODEL;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack.BLL
{
    public class Event_Enable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory() + "\\仙战";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(path + "\\技能卡");
                    Directory.CreateDirectory(path + "\\游戏配置");
                    Directory.CreateDirectory(path + "\\奇遇");
                }
                GeneralControl.CQApi = e.CQApi;
                GeneralControl.CQLog = e.CQLog;
                Init init = new Init();
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
