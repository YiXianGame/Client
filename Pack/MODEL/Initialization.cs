using Make.MODEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack.MODEL
{
    public class Initialization
    {
        public Initialization()
        {
            string path = GeneralControl.directory;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + "\\技能卡");
                Directory.CreateDirectory(path + "\\奇遇");
            }
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
                foreach (SkillCard item in skillCardsModel.SkillCards)
                {
                    List<State> List = new List<State>();
                    foreach (State item_1 in item.Effect_States)
                    {

                        State state = new State();

                    }
                }

                GeneralControl.Skill_Cards.Add(skillCardsModel);
                foreach (SkillCard item in skillCardsModel.SkillCards)
                {
                    GeneralControl.Skill_Card_Dictionary.Add(item.Name, item);
                }
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
                GeneralControl.Adventures.Add(adventure);
            }
        }
    }
}
