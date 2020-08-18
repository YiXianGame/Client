
using Make.MODEL;
using Native.Sdk.Cqp;
using Newtonsoft.Json;
using Pack.Element;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            GeneralControl.socket.Request_Client("CQ$"+frompersonal.ToString()+ "|" + fromgroup.ToString() + "|" + msg);
        }
        public static void Send_To_Server(string msg)
        {
            GeneralControl.socket.Request_Client("客户端$" + msg);
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
        public static void Send_To_CQ_XML(string msg, long frompersonal, long fromgroup = -1)
        {
            if (fromgroup == -1)
            {
                GeneralControl.CQApi.SendPrivateMessage(frompersonal, "[CQ:xml,data=" + escape(msg) + "]" );
            }
            else
            {
                GeneralControl.CQApi.SendPrivateMessage(frompersonal, "[CQ:xml,data=" + escape(msg) + "]");
            }
        }
        private static string escape(string msg)
        {
            return msg.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;").Replace(",", "&#44;");
        }
        public static void Client_Recieve_Server(string msg)
        {
            string[] data = msg.Split('#');
            if (data.Length > 0)
            {
                if(data.Length==2 && data[0] == "用户")
                {
                    if (data[1] != null)
                    {
                        GeneralControl.Menu_Person_Informations_Class.Instance.Author = JsonConvert.DeserializeObject<Make.MODEL.Author>(data[1]);                        
                    }
                }
                else if (data.Length == 2 && data[0] == "作者查询")
                {
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        if (data[1] != null)
                        {
                            GeneralControl.MainMenu.CardPanle.Author.DataContext = JsonConvert.DeserializeObject<Make.MODEL.Author>(data[1]);
                            GeneralControl.MainMenu.AdventurePanle.Author.DataContext = JsonConvert.DeserializeObject<Make.MODEL.Author>(data[1]);
                        }
                    });
                }
                else if (data.Length == 2 && data[0] == "更新卡牌版本")
                {
                    GeneralControl.Skill_Card_Date = DateTime.Parse(data[1]);
                }
                else if (data.Length == 2 && data[0] == "更新奇遇版本")
                {
                    GeneralControl.Adventure_Date = DateTime.Parse(data[1]);
                }
                else if(data.Length==2 && data[0] == "获取技能卡")
                {
                    List<SkillCardsModel> skillCardsModels = JsonConvert.DeserializeObject<List<SkillCardsModel>>(data[1]);
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        skillCardsModels.ForEach(delegate (SkillCardsModel item) { GeneralControl.MainMenu.CardPanle.Add_Card(item);});
                    });                   
                }
                else if (data.Length == 2 && data[0] == "获取奇遇")
                {
                    GeneralControl.Adventures.ForEach((Action<Adventure>)delegate (Adventure item) { item.Cloud = "非云端"; });
                    List<Adventure> Adventures = JsonConvert.DeserializeObject<List<Adventure>>(data[1]);
                    foreach (Adventure item in Adventures)
                    {
                        item.Cloud = "云端";
                        GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                        {
                            IEnumerable<Custom_Card_Adventure> ienum = from Custom_Card_Adventure ienum_item in GeneralControl.MainMenu.AdventurePanle.AdventurePanel.Children where item.ID == ienum_item.AdventureCard.ID select ienum_item;
                            if (ienum.Any())
                            {
                                Custom_Card_Adventure adventure = ienum.First();
                                GeneralControl.Adventures.Remove(adventure.AdventureCard);
                                adventure.AdventureCard = item;
                                item.Save();
                            }
                            else
                            {
                                GeneralControl.MainMenu.AdventurePanle.Add_Adventure(item);
                                item.Add_To_General();
                                item.Save();
                            }
                        });
                    }
                }
            }
        }
    }
}
