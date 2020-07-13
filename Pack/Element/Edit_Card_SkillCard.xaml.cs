using Make.MODEL;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pack.Element
{

    /// <summary>
    /// Edit_Card.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_Card_SkillCard : UserControl
    {
        Custom_Card_SkillCard Origin_Custom_Card;
        public Edit_Card_SkillCard()
        {
            InitializeComponent();
        }

        public void Open_Edit(Custom_Card_SkillCard custom_Card)
        {
            Is_Basic_Element.DataContext = custom_Card.SkillCardsModel;
            Origin_Custom_Card = custom_Card;
            Custom_Card.SkillCardsModel = custom_Card.SkillCardsModel;
            Custom_Card.DataContext = custom_Card.SkillCardsModel.SkillCards[0];
            Visibility = Visibility.Visible;
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            States_Select.Visibility = Visibility.Visible;
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Origin_Custom_Card.SkillCardsModel.Save();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            DependencyObject ptr = sender as DependencyObject;
            while (!(ptr is MainWindow)) ptr = VisualTreeHelper.GetParent(ptr);
            MainWindow  mainWindow  = ptr as MainWindow;
            Origin_Custom_Card.SkillCardsModel.Delete();
            Debug.WriteLine(GeneralControl.Skill_Cards.Count);
            GeneralControl.Skill_Cards.Remove(Origin_Custom_Card.SkillCardsModel);
            foreach(SkillCard item in Origin_Custom_Card.SkillCardsModel.SkillCards)
            {
                GeneralControl.Skill_Card_Dictionary.Remove(item.Name);
            }
            Debug.WriteLine(GeneralControl.Skill_Cards.Count);
            mainWindow.CardPanle.CardsPanel.Children.Remove(Origin_Custom_Card);
            mainWindow.BasicCardsPanle.Basic_CardsPanel.Children.Remove(Origin_Custom_Card);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Effect_States.Remove((State)(((Button)sender).DataContext));
            SkillCard skillCard = new SkillCard
            {
                Is_Cure = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Cure,
                Is_Eternal = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Eternal,
                Is_Physics = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Physics,
                Is_Magic = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Magic,
                Is_Attack = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Attack
            };
            Custom_Card.DataContext = skillCard;
            Custom_Card.DataContext = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1];
            Origin_Custom_Card.DataContext = skillCard;
            Origin_Custom_Card.DataContext = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1];
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject ptr = sender as DependencyObject;
            while (!(ptr is MainWindow)) ptr = VisualTreeHelper.GetParent(ptr);
            MainWindow mainWindow = ptr as MainWindow;
            if (Origin_Custom_Card.SkillCardsModel.Is_Basic)
            {
                mainWindow.BasicCardsPanle.Add_Card(Origin_Custom_Card.SkillCardsModel);
            }
            else
            {
                foreach (Custom_Card_Basic_SkillCard skillCardsModel in mainWindow.BasicCardsPanle.Basic_CardsPanel.Children)
                {
                    if (skillCardsModel.SkillCardsModel.Equals(Origin_Custom_Card.SkillCardsModel))
                    {
                        mainWindow.BasicCardsPanle.Basic_CardsPanel.Children.Remove(skillCardsModel);
                        break;
                    }
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Effect_States.Count >= Make.MODEL.GeneralControl.MaxStates)
            {
                MessageBox.Show("状态数量已满");
                return;
            }
            State state = new State
            {
                Name = (sender as Button).Content.ToString(),
                Duration_Immediate = 10,
                Duration_Round = 1
            };
            Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Effect_States.Add(state);
            SkillCard skillCard = new SkillCard
            {
                Is_Cure = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Cure,
                Is_Eternal = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Eternal,
                Is_Physics = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Physics,
                Is_Magic = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Magic,
                Is_Attack = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1].Is_Attack
            };
            Custom_Card.DataContext = skillCard;
            Custom_Card.DataContext = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1];
            Origin_Custom_Card.DataContext = skillCard;
            Origin_Custom_Card.DataContext = Custom_Card.SkillCardsModel.SkillCards[Custom_Card.Rate.Value - 1];
            States_Select.Visibility = Visibility.Hidden;
        }
    }
}
