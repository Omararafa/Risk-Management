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
    /// Interaction logic for Window02.xaml
    /// </summary>
    public partial class Window02 : UserControl
    {
        public Window02()
        {
            InitializeComponent();
        }

        public int Height { get; internal set; }
        public int Width { get; internal set; }
        public double Left { get; internal set; }
        public double Top { get; internal set; }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
