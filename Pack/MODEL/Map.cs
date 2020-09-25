using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make.MODEL
{
    public abstract class  Map
    {
        private Pos[,] pos_Map;
        public Pos[,] Pos_Map { get => pos_Map; set => pos_Map = value; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Current_Resources_Monster { get; set; } = 0;
        public int Current_Resources_SkillCard { get; set; } = 0;
        public int Current_Resources_State { get; set; } = 0;
        public int Current_Resources_Adventure { get; set; } = 0;
        public List<Player> Players = new List<Player>();
      
    }
}
