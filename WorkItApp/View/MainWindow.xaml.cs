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
using System.Windows.Threading;
using WorkItApp.Controller;

namespace WorkItApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public static IController controller;
        public MainWindow()
        {
            InitializeComponent();
        }
        public static IController getController()
        {
            return controller;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Customers_Managment_Click(object sender, RoutedEventArgs e)
        {
            Customers_Window customers_window = new Customers_Window(this);
            customers_window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Workers_Managment_Click(object sender, RoutedEventArgs e)
        {
            Workers_Window workers_window = new Workers_Window(this);
            workers_window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Activities_Managment_Click(object sender, RoutedEventArgs e)
        {
            Activities_Window activities_window = new Activities_Window(this);
            activities_window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Items_Managment_Click(object sender, RoutedEventArgs e)
        {
            Items_Window items_window = new Items_Window(this);
            items_window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Bonuses_Managment_Click(object sender, RoutedEventArgs e)
        {
            Bonuses_Window bonuses_window = new Bonuses_Window(this);
            bonuses_window.Show();
            this.Visibility = Visibility.Hidden;
        }

        public void setController(IController cont)
        {
            controller = cont;
        }
    }
}
