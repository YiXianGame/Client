
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
                GeneralControl.CQApi.SendGroupMessage(fromgroup, "[CQ:xml,data=" + escape(msg) + "]");
            }
        }
        public static void Send_To_CQ_JSON(string msg, long frompersonal, long fromgroup = -1)
        {
            if (fromgroup == -1)
            {
                GeneralControl.CQApi.SendPrivateMessage(frompersonal, "[CQ:app,data=" + escape(msg) + "]");
            }
            else
            {
                GeneralControl.CQApi.SendGroupMessage(fromgroup, "[CQ:app,data=" + escape(msg) + "]");
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
                else if (data.Length == 2 && data[0] == "开始更新卡牌版本")
                {
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        (from Custom_Card_SkillCard item in GeneralControl.MainMenu.CardPanle.CardsPanel.Children where item.SkillCardsModel.Cloud == "云端" select item).ToList().ForEach(delegate (Custom_Card_SkillCard item)
                        {
                            item.SkillCardsModel.Delete();
                            GeneralControl.MainMenu.CardPanle.CardsPanel.Children.Remove(item);
                        });
                    });
                    GeneralControl.Skill_Card_Date = DateTime.Parse(data[1]);
                }
                else if (data.Length == 2 && data[0] == "开始更新奇遇版本")
                {
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        (from Custom_Card_Adventure item in GeneralControl.MainMenu.AdventurePanle.AdventurePanel.Children where item.AdventureCard.Cloud == "云端" select item).ToList().ForEach(delegate (Custom_Card_Adventure item)
                        {
                            item.AdventureCard.Delete();
                            GeneralControl.MainMenu.AdventurePanle.AdventurePanel.Children.Remove(item);
                        });
                    });
                    GeneralControl.Adventure_Date = DateTime.Parse(data[1]);
                }
                else if(data.Length==2 && data[0] == "获取技能卡")
                {
                    List<SkillCardsModel> skillCardsModels = JsonConvert.DeserializeObject<List<SkillCardsModel>>(data[1]);
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        skillCardsModels.ForEach(delegate (SkillCardsModel item) 
                        {                            
                            Custom_Card_SkillCard skillCardsModel;
                            skillCardsModel = (from Custom_Card_SkillCard gene_skill in GeneralControl.MainMenu.CardPanle.CardsPanel.Children where gene_skill.SkillCardsModel.ID == item.ID select gene_skill).FirstOrDefault();
                            if (skillCardsModel!=null)
                            {
                                foreach (SkillCard obj in skillCardsModel.SkillCardsModel.SkillCards)
                                {
                                    GeneralControl.Skill_Card_Dictionary.Remove(obj.Name);
                                }
                                GeneralControl.Skill_Cards.Remove(skillCardsModel.SkillCardsModel);
                                GeneralControl.MainMenu.CardPanle.CardsPanel.Children.Remove(skillCardsModel);
                            }
                            GeneralControl.MainMenu.CardPanle.Add_Card(item);
                            item.Save();
                        });
                    });                   
                }
                else if (data.Length == 2 && data[0] == "获取奇遇")
                {
                    List<Adventure> Adventures = JsonConvert.DeserializeObject<List<Adventure>>(data[1]);
                    GeneralControl.MainMenu.Dispatcher.Invoke((Action)delegate ()
                    {
                        Adventures.ForEach(delegate (Adventure item)
                        {
                            Custom_Card_Adventure adventure;
                            adventure = (from Custom_Card_Adventure gene_skill in GeneralControl.MainMenu.AdventurePanle.AdventurePanel.Children where gene_skill.AdventureCard.ID == item.ID select gene_skill).FirstOrDefault();
                            if (adventure != null)
                            {
                                GeneralControl.Adventures.Remove(adventure.AdventureCard);
                                GeneralControl.MainMenu.AdventurePanle.AdventurePanel.Children.Remove(adventure);
                            }
                            GeneralControl.MainMenu.AdventurePanle.Add_Adventure(item);
                            item.Save();
                        });
                    });
                }
            }
        }
    }
}
