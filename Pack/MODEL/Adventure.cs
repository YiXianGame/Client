using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Make.MODEL
{
    /// <summary>
    /// 奇遇
    /// </summary>
    public class Adventure
    {
        private string name = "";//奇遇名
        private int mp;//对这个玩家造成的MP
        private int hp;//对这个玩家造成的HP
        private int state=-1;//奇遇状态（是否可用）
        private List<State> effect_States=new  List<State>();//奇遇所自带的状态效果
        private string messages="";//自带信息
        private int probability;//概率
        private string description="";//奇遇的描述（介绍）

        public string Name
        { 
            get => name;
            set
            {
                if((from Adventure item in GeneralControl.Adventures where item.Name==value select item).Any())
                {
                    MessageBox.Show("发现重复命名奇遇【" + value + "】");
                    return;
                }
                name = value;
            }
        }
        public int Mp { get => mp; set => mp = value; }
        public int Hp { get => hp; set => hp = value; }

        public string Messages { get => messages; set => messages = value; }
        public int Probability { get => probability; set => probability = value; }
        public List<State> Effect_States { get => effect_States; set => effect_States = value; }
        public int State { get => state; set => state = value; }
        public string Description { get => description; set => description = value; }
        public void SetName(string adventure_Name)
        {
            name = adventure_Name;
        }
        public Adventure()
        {

            
        }
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            string filepath = GeneralControl.directory + "\\奇遇\\" + Name + ".json";
            File.WriteAllText(filepath, json);
        }

        public void Delete()
        {
            string filepath = GeneralControl.directory + "\\奇遇\\" + Name + ".json";
            File.Delete(filepath);
        }
    }
}
