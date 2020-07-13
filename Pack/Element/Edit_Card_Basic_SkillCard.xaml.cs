using Make.MODEL;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
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
    public partial class Edit_Card_Basic_SkillCard : UserControl
    {
        Custom_Card_Basic_SkillCard Origin_Custom_Basic_Card;
        public Edit_Card_Basic_SkillCard()
        {
            InitializeComponent();
            
        }

        public void Open_Edit(Custom_Card_Basic_SkillCard custom_Card_Basic_SkillCard)
        {
            Origin_Custom_Basic_Card = custom_Card_Basic_SkillCard;
            Custom_Basic_Card.SkillCardsModel = custom_Card_Basic_SkillCard.SkillCardsModel;
            Custom_Basic_Card.DataContext = Custom_Basic_Card.SkillCardsModel.SkillCards[0];
            Is_Basic.DataContext = Origin_Custom_Basic_Card.SkillCardsModel;
            Visibility = Visibility.Visible;   
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Custom_Basic_Card.DataContext != null)
            {
                State state = (State)((ComboBox)sender).DataContext;
                state.Name = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            States_Select.Visibility = Visibility.Visible;
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Origin_Custom_Basic_Card.SkillCardsModel.Save();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            DependencyObject ptr = sender as DependencyObject;
            while (!(ptr is MainWindow)) ptr = VisualTreeHelper.GetParent(ptr);
            MainWindow mainWindow = ptr as MainWindow;
            mainWindow.BasicCardsPanle.Basic_CardsPanel.Children.Remove(Origin_Custom_Basic_Card);
            foreach (Custom_Card_Basic_SkillCard skillCardsModel in mainWindow.CardPanle.CardsPanel.Children)
            {
                if (skillCardsModel.SkillCardsModel.Equals(Origin_Custom_Basic_Card.SkillCardsModel))
                {
                    mainWindow.CardPanle.CardsPanel.Children.Remove(skillCardsModel);
                    break;
                }
            }
            GeneralControl.Skill_Cards.Remove(Origin_Custom_Basic_Card.SkillCardsModel);
            foreach (SkillCard item in Origin_Custom_Basic_Card.SkillCardsModel.SkillCards)
            {
                GeneralControl.Skill_Card_Dictionary.Remove(item.Name);
            }
            Origin_Custom_Basic_Card.SkillCardsModel.Delete();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Effect_States.Remove((State)(((Button)sender).DataContext));
            SkillCard skillCard = new SkillCard
            {
                Is_Cure = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Cure,
                Is_Eternal = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Eternal,
                Is_Physics = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Physics,
                Is_Magic = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Magic,
                Is_Attack = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Attack
            };
            Custom_Basic_Card.DataContext = skillCard;
            Custom_Basic_Card.DataContext = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1];
            Origin_Custom_Basic_Card.DataContext = skillCard;
            Origin_Custom_Basic_Card.DataContext = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1];
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject ptr = sender as DependencyObject;
            while (!(ptr is Basic_CardPanle)) ptr = VisualTreeHelper.GetParent(ptr);
            Basic_CardPanle cardPanle = ptr as Basic_CardPanle;
            if (Origin_Custom_Basic_Card.SkillCardsModel.Is_Basic)
            {
                Origin_Custom_Basic_Card = cardPanle.Add_Card(Origin_Custom_Basic_Card.SkillCardsModel);
            }
            else
            {
                cardPanle.Basic_CardsPanel.Children.Remove(Origin_Custom_Basic_Card);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Effect_States.Count >= Make.MODEL.GeneralControl.MaxStates)
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
            Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Effect_States.Add(state);
            SkillCard skillCard = new SkillCard
            {
                Is_Cure = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Cure,
                Is_Eternal = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Eternal,
                Is_Physics = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Physics,
                Is_Magic = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Magic,
                Is_Attack = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1].Is_Attack
            };
            Custom_Basic_Card.DataContext = skillCard;
            Custom_Basic_Card.DataContext = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1];
            Origin_Custom_Basic_Card.DataContext = skillCard;
            Origin_Custom_Basic_Card.DataContext = Custom_Basic_Card.SkillCardsModel.SkillCards[Custom_Basic_Card.Rate.Value - 1];
            States_Select.Visibility = Visibility.Hidden;
        }
    }
}
