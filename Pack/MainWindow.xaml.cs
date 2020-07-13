using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pack.Element;
using Make;
using Make.MODEL;
using MaterialDesignThemes.Wpf;
using System.IO;
using Pack.MODEL;

namespace Pack
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Make.MODEL.SkillCardsModel SkillCard_1;
        public MainWindow()
        {
            InitializeComponent(); 
            Init();
        }

        private void Init()
        {
            Initialization initialization = new Initialization();//游戏初始化入口
            UI_Init();
            Menu_Adventure_Cards.DataContext = Make.MODEL.GeneralControl.Menu_Adventure_Cards_Class.Instance;
            Menu_Lience.DataContext = GeneralControl.Menu_Lience_Class.Instance;
            Menu_Person_Informations.DataContext = GeneralControl.Menu_Person_Informations_Class.Instance;
            Menu_Version_Informations.DataContext = GeneralControl.Menu_Version_Informations_Class.Instance;
            Menu_Skill_Cards.DataContext = GeneralControl.Menu_Skill_Cards_Class.Instance;
        }

        private void SkillCardsPanle_Initialized(object sender, EventArgs e)
        {

        }

        private void SkillCardsPanle_Initialized_1(object sender, EventArgs e)
        {

        }

        private void Menu_Button_3_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Menu_Button_4_Click(object sender, RoutedEventArgs e)
        {
    
        }
        private void Menu_Button_1_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 0;
        }
        private void Menu_Button_1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 1;
        }

        private void Menu_Button_1_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 2;
        }

        private void Menu_Button_1_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 3;
        }

        private void Menu_Button_1_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 4;
        }

        private void Menu_Button_1_Copy4_Click(object sender, RoutedEventArgs e)
        {   
            Main_TabControl.SelectedIndex = 5;
        }

        private void Menu_Button_1_Copy5_Click(object sender, RoutedEventArgs e)
        {
            Main_TabControl.SelectedIndex = 6;
        }
        private void UI_Init()
        {
            //初始化技能面板
            foreach(SkillCardsModel skillCardsModel in GeneralControl.Skill_Cards)
            {
                CardPanle.Add_Card(skillCardsModel);
            }
            //初始化基础卡
            foreach (SkillCardsModel skillCardsModel in Make.MODEL.Filter.Basic_SkillCardsModel(GeneralControl.Skill_Cards))
            {
                BasicCardsPanle.Add_Card(skillCardsModel);
            }
            //初始化奇遇
            foreach (Adventure adventure in Make.MODEL.GeneralControl.Adventures)
            {
                AdventurePanle.Add_Adventure(adventure);
            }
        }
    }
}
