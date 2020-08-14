using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Make.MODEL
{
    public abstract class Room
    {
        private int round=0;//房间回合
        private List<Player> players=new  List<Player>();//房间内的玩家
        private Enums.Room stage = Enums.Room.Wait;//房间阶段
        private Enums.Room_Type type = Enums.Room_Type.Solo;//房间类型
        private long from_Group;//房间群号
        private int deaths=0;//死亡总数
        private DateTime latest_Date=DateTime.Now;//房间最新时间
        private int max_Personals;//最大玩家数
        private int min_Personals;//最少玩家数
        private int current_Personals;//当前玩家数
        private Socket socket;
        public int Round { get => round; set => round = value; }
        public List<Player> Players { get => players; set => players = value; }
        public Enums.Room Stage { get => stage; set => stage = value; }
        public Enums.Room_Type Type { get => type; set => type = value; }
        public long From_Group { get => from_Group; set => from_Group = value; }
        public int Deaths { get => deaths; set => deaths = value; }
        public DateTime Latest_Date { get => latest_Date; set => latest_Date = value; }
        public int Max_Personals { get => max_Personals; set => max_Personals = value; }
        public int Current_Personals { get => current_Personals; set => current_Personals = value; }
        public int Min_Personals { get => min_Personals; set => min_Personals = value; }
        public Socket Socket { get => socket; set => socket = value; }
      
    }
}
