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
using System.Windows.Shapes;
using WorkItApp.Controller;

namespace WorkItApp.View
{
    /// <summary>
    /// Interaction logic for Workers_Window.xaml
    /// </summary>
    public partial class Workers_Window : Window
    {
        Window MainWindow;
        public Workers_Window(Window Main)
        {
            this.MainWindow = Main;
            InitializeComponent();
        }

        private void Back_to_Main_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
