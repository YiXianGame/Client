using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make.MODEL
{
    /// <summary>
    /// 这里根据enum的特性，虽然类无加载，但其实enum这个枚举表已经存在。
    /// </summary>
    public class Enums
    {
        public enum Room_Type { Solo,Team, Battle_Royale}
        public enum Room {  Wait, Raise, Action, Result };
        public enum Power { Human, Monster, Neutral};
        public enum Skill_Card_State {Disable,Enable,Sell };
        public enum Player_Active { 仙域大陆, 回合单挑 };
    }
}
