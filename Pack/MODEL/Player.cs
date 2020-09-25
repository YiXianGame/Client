using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Make.MODEL
{
    public class Player 
    {
        private long iD=-1;//玩家ID也是QQ号（-1时为机器人）
        private string name;//玩家昵称
        private int hp;//血量
        private int mp;//能量
        private Room room;//房间ID
        private int battle_Count;//战斗场次
        private int balances;//金钱
        private int lv;//等级
        private string title;//称号
        private Enums.Race race;//种族
        private List<Player> directs = new List<Player>();//玩家锁定
        private bool action;//是否出招
        private int team;//所属队伍
        private Pos pos;//地标
        private Map map;//地图
        private List<Player> directsed = new List<Player>();
        private int exp;//经验
        private List<State> states = new List<State>();//战斗状态
        private bool is_Death;//是否死亡
        private List<Player> friends = new List<Player>();//队友
        private Enums.Player_Active active = Enums.Player_Active.Leisure ;//玩家当前游戏状态
        private int kills;//击杀数
        private int deaths;//死亡数
        private int death_Round;//死亡回合
        private DateTime skillCards_Date;//技能卡版本
        private DateTime registration_date;//注册时间
        private List<SkillCard> repository_SkillCards= new List<SkillCard>();//技能卡仓库
        private List<SkillCard> battle_SkillCards = new List<SkillCard>();//备战的技能卡
        private List<SkillCard> hand_SkillCards = new List<SkillCard>();//手中的技能卡
        private SkillCard action_Skill;//释放的技能
        private long from_Group=-1;//QQ群号
        private DateTime leisure;
        private Socket socket;
        private bool is_Robot=false;
        public long ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public List<Player> Directsed { get => directsed; set => directsed = value; }
        public int Hp 
        { 

            get => hp;
            set 
            { 
                hp = value; 
                if (hp <= 0)
                {                    
                    Is_Death = true;
                    Deaths++;
                }
                else 
                { 
                    Is_Death = false;
                    Deaths = 0;
                }
             }
        }
        public int Mp { get => mp; set => mp = value; }
        public int Battle_Count { get => battle_Count; set => battle_Count = value; }
        public int Balances { get => balances; set => balances = value; }
        public int Lv { get => lv; set => lv = value; }
        public string Title { get => title; set => title = value; }
        public List<State> States { get => states; set => states = value; }
        public bool Is_Death { get => is_Death; set => is_Death = value; }

        public int Kills { get => kills; set => kills = value; }
        public int Deaths { get => deaths; set => deaths = value; }
        public int Death_Round { get => death_Round; set => death_Round = value; }
        
        public DateTime SkillCards_Date { get => skillCards_Date; set => skillCards_Date = value; }
        public DateTime Registration_date { get => registration_date; set => registration_date = value; }
        public List<SkillCard> Repository_SkillCards { get => repository_SkillCards; set => repository_SkillCards = value; }
        public List<SkillCard> Battle_SkillCards { get => battle_SkillCards; set => battle_SkillCards = value; }
        public List<SkillCard> Hand_SkillCards { get => hand_SkillCards; set => hand_SkillCards = value; }
        public Room Room { get => room; set => room = value; }
        public SkillCard Action_Skill { get => action_Skill; set => action_Skill = value; }
        public Enums.Race Race { get => race; set => race = value; }
        public bool Action { get => action; set => action = value; }
        public Enums.Player_Active Active { get => active; set => active = value; }
        public int Team { get => team; set => team = value; }
        public int Exp 
        { 
            get => exp;
            set
            {
                exp = value;
                if (exp < 10)
                {
                    Title = "炼气";
                    Lv = 1;
                }
                else if (exp >= 10)
                {
                    Title = "筑基";
                    Lv = 2;
                }
                else if (exp >= 100)
                {
                    Title = "金丹";
                    Lv = 3;
                }
                else if (exp >= 500)
                {
                    Title = "元婴";
                    Lv = 4;
                }
                else if (exp >= 1000)
                {
                    Title = "分神";
                    Lv =5;
                }
                else if (exp >= 1500)
                {
                    Title = "洞虚";
                    Lv = 6;
                }
                else if (exp >= 2000)
                {
                    Title = "大乘";
                    Lv = 7;
                }
                else if (exp >= 3000)
                {
                    Lv = 8;
                    Title = "羽化";
                }
            }
        }

        public long From_Group { get => from_Group; set => from_Group = value; }
        public Socket Socket { get => socket; set => socket = value; }
        public Pos Pos { get => pos; set => pos = value; }
        public Map Map { get => map; set => map = value; }
        public List<Player> Directs { get => directs; set => directs = value; }
        public bool Is_Robot { get => is_Robot; set => is_Robot = value; }
        public DateTime Leisure { get => leisure; set => leisure = value; }
        public List<Player> Friends { get => friends; set => friends = value; }

    }
}
