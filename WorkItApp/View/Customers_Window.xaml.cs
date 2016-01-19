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
    /// Interaction logic for Customers_Window.xaml
    /// </summary>
    public partial class Customers_Window : Window
    {
        Window MainWindow;
        public Customers_Window(Window Main)
        {
            this.MainWindow = Main;
            InitializeComponent();
        }

        public void Back_to_Main_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            this.Close();
        }

        private void Add_Cust_Btn_Click(object sender, RoutedEventArgs e)
        {
            Cust_Frame.Content = new Cust_Page(this);
        }

        private void Add_Bonus_to_Cust_Btn_Click(object sender, RoutedEventArgs e)
        {
            Cust_Frame.Content = new BenefitPage(this);
        }

        private void Sell_Item_to_Cust_Btn_Click(object sender, RoutedEventArgs e)
        {
            Cust_Frame.Content = new Items_Sale_Page(this);
        }
    }
}
