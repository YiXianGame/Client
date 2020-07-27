using Make.MODEL;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pack.Element
{

    /// <summary>
    /// Edit_Card.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_Card_Adventure : UserControl
    {
        Custom_Card_Adventure Origin_Custom_Card;
        public Edit_Card_Adventure()
        {
            InitializeComponent();
        }


        public void Open_Edit(Custom_Card_Adventure adventureCard)
        {
            Origin_Custom_Card = adventureCard;
            Custom_Card_Adventure.AdventureCard = adventureCard.AdventureCard;
            Custom_Card_Adventure.DataContext = adventureCard.AdventureCard;
            Visibility = Visibility.Visible;            
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Custom_Card_Adventure.DataContext != null)
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
            //**保存
            this.Visibility = Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Custom_Card_Adventure.AdventureCard.Effect_States.Remove((State)(((Button)sender).DataContext));
            Adventure skillCard = new Adventure();
            Custom_Card_Adventure.DataContext = skillCard;
            Custom_Card_Adventure.DataContext = Custom_Card_Adventure.AdventureCard;
            Origin_Custom_Card.DataContext = skillCard;
            Origin_Custom_Card.DataContext = Custom_Card_Adventure.AdventureCard;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            DependencyObject ptr = sender as DependencyObject;
            while (!(ptr is AdventurePanle)) ptr = VisualTreeHelper.GetParent(ptr);
            AdventurePanle cardPanle = ptr as AdventurePanle;
            cardPanle.AdventurePanel.Children.Remove(Origin_Custom_Card);
            GeneralControl.Adventures.Remove(Origin_Custom_Card.AdventureCard);
            Origin_Custom_Card.AdventureCard.Delete();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Custom_Card_Adventure.AdventureCard.Effect_States.Count >= Make.MODEL.GeneralControl.MaxStates)
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
            Custom_Card_Adventure.AdventureCard.Effect_States.Add(state);
            Adventure adventure = new Adventure();
            Custom_Card_Adventure.DataContext = adventure;
            Custom_Card_Adventure.DataContext = Custom_Card_Adventure.AdventureCard;
            Origin_Custom_Card.DataContext = adventure;
            Origin_Custom_Card.DataContext = Custom_Card_Adventure.AdventureCard;
            States_Select.Visibility = Visibility.Hidden;
        }
    }
}
