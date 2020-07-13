using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make.MODEL
{
    public static class GeneralControl
    {
        public static int MaxLevel=5;   
        public static int MaxStates = 9;
        public static bool LazyLoad_SkillCards = false;
        public static List<SkillCardsModel> Skill_Cards = new  List<SkillCardsModel>();//总引用,但UI层还有一层引用，删掉的同时记得删掉UI层
        public static Dictionary<string, SkillCard> Skill_Card_Dictionary = new Dictionary<string, SkillCard>();
        public static List<Adventure> Adventures = new  List<Adventure>();//总引用,但UI层还有一层引用，删掉的同时记得删掉UI层
        public static Dictionary<long,Player> Players = new Dictionary<long, Player>();
        public static int Seconds_To_Round=10;
        public static string directory = System.IO.Directory.GetCurrentDirectory() + "\\app\\仙战";
        public class Menu_Skill_Cards_Class
        {
            private static readonly Menu_Skill_Cards_Class _instance = null;
            static Menu_Skill_Cards_Class()
            {
                _instance = new Menu_Skill_Cards_Class();
            }
            private Menu_Skill_Cards_Class()
            {

            }
            public static Menu_Skill_Cards_Class Instance
            {
                get { return _instance; }
            }
        }
        public class Menu_Adventure_Cards_Class
        {
            private static readonly Menu_Adventure_Cards_Class _instance = null;
            static Menu_Adventure_Cards_Class()
            {
                _instance = new Menu_Adventure_Cards_Class();
            }
            private Menu_Adventure_Cards_Class()
            {

            }
            public static Menu_Adventure_Cards_Class Instance
            {
                get { return _instance; }
            }
        }
        public class Menu_Version_Informations_Class
        {
            private string expiration_Date = DateTime.Now.ToString();
            private static readonly Menu_Version_Informations_Class _instance = null;
            static Menu_Version_Informations_Class()
            {
                _instance = new Menu_Version_Informations_Class();
            }
            private Menu_Version_Informations_Class()
            {

            }
            public static Menu_Version_Informations_Class Instance
            {
                get { return _instance; }
            }

            public string Expiration_Date { get => expiration_Date; set => expiration_Date = value; }
        }
        public class Menu_Person_Informations_Class
        {
            private string user_Name = "剑仙";
            private static readonly Menu_Person_Informations_Class _instance = null;
            static Menu_Person_Informations_Class()
            {
                _instance = new Menu_Person_Informations_Class();
            }
            private Menu_Person_Informations_Class()
            {

            }
            public static Menu_Person_Informations_Class Instance
            {
                get { return _instance; }
            }
            public string User_Name { get => user_Name; set => user_Name = value; }
        }
        public class Menu_Lience_Class
        {
            private static readonly Menu_Lience_Class _instance = null;
            static Menu_Lience_Class()
            {
                _instance = new Menu_Lience_Class();
            }
            private Menu_Lience_Class()
            {

            }
            public static Menu_Lience_Class Instance
            {
                get { return _instance; }
            }
        }
    }
}