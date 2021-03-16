using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Risk_Management
{
    /// <summary>
    /// Interaction logic for XamlLocation.xaml
    /// </summary>
    public partial class Trades : Window
    {

        public string Trade="";
        public string ExcelLocation = "";
        public bool AllChecked = false;
        public Trades()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Trade = "";
            Close();
        }

        private void P1_Checked(object sender, RoutedEventArgs e)
        {
            if (!(AllChecked == true))
            {
                AllChecked = false;
                P2.IsChecked = false;
                P3.IsChecked = false;
                P4.IsChecked = false;
                P5.IsChecked = false;
                P6.IsChecked = false;
                Trade = P1.Content.ToString();
            }

        }

        private void P2_Checked(object sender, RoutedEventArgs e)
        {
            if (!(AllChecked == true))
            {
                AllChecked = false;
                P1.IsChecked = false;
                P3.IsChecked = false;
                P4.IsChecked = false;
                P5.IsChecked = false;
                P6.IsChecked = false;
                Trade = P2.Content.ToString();
            }
                
        }

        private void P3_Checked(object sender, RoutedEventArgs e)
        {
            if (!(AllChecked == true))
            {
                AllChecked = false;
                P2.IsChecked = false;
                P1.IsChecked = false;
                P4.IsChecked = false;
                P5.IsChecked = false;
                P6.IsChecked = false;
                Trade = P3.Content.ToString();
            }
                
        }

        private void P4_Checked(object sender, RoutedEventArgs e)
        {
            if (!(AllChecked == true))
            {
                AllChecked = false;
                P2.IsChecked = false;
                P3.IsChecked = false;
                P1.IsChecked = false;
                P5.IsChecked = false;
                P6.IsChecked = false;
                Trade = P4.Content.ToString();
            }
                
        }

        private void P5_Checked(object sender, RoutedEventArgs e)
        {
            if (!(AllChecked == true))
            {
                AllChecked = false;
                P2.IsChecked = false;
                P3.IsChecked = false;
                P4.IsChecked = false;
                P1.IsChecked = false;
                P6.IsChecked = false;
                Trade = P5.Content.ToString();
            }
                
        }

        private void P6_Checked(object sender, RoutedEventArgs e)
        {
            AllChecked = true;
            P1.IsChecked = true;
            P2.IsChecked = true;
            P3.IsChecked = true;
            P4.IsChecked = true;
            P5.IsChecked = true;
            Trade = "All Trades";
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool Checkk = false;
            Close();
            if(P1.IsChecked==true|| P2.IsChecked == true|| P3.IsChecked == true|| P4.IsChecked == true|| P5.IsChecked == true)
            {
                Checkk = true;
            }
            if (Checkk == true)
            {
                Properties.Settings.Default.Trades = Trade;
                #region Questionnare window
                Questionnare x = new Questionnare(Trade);
                x.Height = 530;
                x.Width = 600;
                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = x.Width;
                double windowHeight = x.Height;
                x.Left = (screenWidth / 2) - (windowWidth / 2);
                x.Top = (screenHeight / 2) - (windowHeight / 2);
                x.ShowDialog();
                ExcelLocation = x.ExcelLocation;
                //Trade = "";
                #endregion
            }
            else
            {
                MessageBox.Show("Please check any of the above trades", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void P6_Unchecked(object sender, RoutedEventArgs e)
        {
            AllChecked = false;
            P1.IsChecked = false;
            P2.IsChecked = false;
            P3.IsChecked = false;
            P4.IsChecked = false;
            P5.IsChecked = false;
            Trade = "";
        }
    }
}
