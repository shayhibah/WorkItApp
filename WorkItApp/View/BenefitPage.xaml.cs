using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WorkItApp.Controller;

namespace WorkItApp.View
{
    /// <summary>
    /// Interaction logic for BenefitPage.xaml
    /// </summary>
    public partial class BenefitPage : Page
    {
        private IController controller;
        private string currentID;
        private Customers_Window Customer_Window;
        public BenefitPage(Customers_Window Customer_Window)
        {
            this.Customer_Window = Customer_Window;
            controller = MainWindow.getController();
            InitializeComponent();
        }

        private void Search_Cust_ID_Click(object sender, RoutedEventArgs e)
        {
            string answer = controller.searchBonusCust(this.tbx_ID.Text);
            if(answer != "")
            {
                MessageBox.Show(answer);
            }
            else
            {
                currentID = this.tbx_ID.Text;
                bonuses_lbx.Items.Clear();
                List<string> bonuses = controller.getCurrentBonuses();
                foreach(string bonus in bonuses)
                {
                    bonuses_lbx.Items.Add(bonus);
                }
                bonuses_lbx.Visibility = Visibility.Visible;
                Select_Bonus_Btn.Visibility = Visibility.Visible;
            }
        }

        private void Select_Bonus_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(this.bonuses_lbx.SelectedItem != null)
            {
                string selectedBonusNumber = this.bonuses_lbx.SelectedItem.ToString().Split('\t')[0];
                string msg = controller.addBonusToCust(currentID, Convert.ToInt32(selectedBonusNumber));
                MessageBox.Show(msg);
                Customer_Window.Back_to_Main_Btn_Click(null,null);
            }
        }
    }
}
