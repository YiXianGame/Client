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
    public class Adventure
    {
        private string name = "";
        private int mp;
        private int hp;
        private int state=-1;//技能卡状态
        private List<State> effect_States=new  List<State>();
        private string messages="";
        private int probability;
        private string description="";

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
