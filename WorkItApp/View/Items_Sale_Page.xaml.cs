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
using WorkItApp.Controller;

namespace WorkItApp.View
{
    /// <summary>
    /// Interaction logic for Items_Sale_Page.xaml
    /// </summary>
    public partial class Items_Sale_Page : Page
    {
        private IController controller;
        private bool correctAmounts = true;
        private Customers_Window Cust_Window;
        private Dictionary<string, int> SoldItems;
        private double total = 0;
        public Items_Sale_Page(Customers_Window Cust_Window)
        {
            this.SoldItems = new Dictionary<string, int>();
            this.Cust_Window = Cust_Window;
            controller = MainWindow.getController();
            InitializeComponent();
        }

        private void Search_Cust_ID_Click(object sender, RoutedEventArgs e)
        {
            string answer = controller.searchItemCust(this.tbx_ID.Text);
            if (answer != "")
            {
                MessageBox.Show(answer);
            }
            else
            {
                this.Items_Table.Visibility = Visibility.Visible;
                List<string> items_list = controller.getItems();
                for(int i=0; i< items_list.Count;i++)
                {
                    string[] itemParts = items_list[i].Split('\t');
                    for(int j = 0; j < itemParts.Length; j++)
                    {
                        TextBlock t = new TextBlock();
                        t.TextAlignment = TextAlignment.Center;
                        t.VerticalAlignment = VerticalAlignment.Center;
                        t.Text = itemParts[j];
                        t.FontWeight = FontWeights.Bold;
                        Grid.SetRow(t, i + 1);
                        Grid.SetColumn(t, 5-j);
                        
                        this.Items_Table.Children.Add(t);
                    }
                }
            }
        }

        private void Calculate_Btn_Click(object sender, RoutedEventArgs e)
        {
            SoldItems.Clear();
            List<Object> results = controller.calculateItemsSale(this);
            string errMsg = (string) results.ElementAt(0);
            total = (double)results.ElementAt(1);
            correctAmounts = (bool)results.ElementAt(2);
            SoldItems = (Dictionary<string, int>)results.ElementAt(3);

            if (correctAmounts)
            {
                this.Total_Sum_Text_Block.Text = Convert.ToString(total);
                this.Confirm_Btn.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        private void Confirm_Btn_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow payment_window = new PaymentWindow();
            payment_window.ShowDialog();
            if (payment_window.getStatus())
            {
                string order_id = controller.updateAmounts(SoldItems, this.tbx_ID.Text, total);
                MessageBox.Show("ההזמנה בוצעה בהצלחה" + "\n" + "מספר ההזמנה: " + order_id);
                Cust_Window.Back_to_Main_Btn_Click(null, null);
            }
        }
    }
}
