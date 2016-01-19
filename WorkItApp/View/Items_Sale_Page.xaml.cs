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
            correctAmounts = true;
            string errMsg = "";
            total = 0;
            for(int i = 1; i <= 5; i++) //Indexes of Items inside Grid
            {
                TextBox t = (TextBox)Items_Table.Children[i + 4];
                int num = 0;
                if(t.Text!= "" && !int.TryParse(t.Text, out num))
                {
                    errMsg+= "ערך של כמות רצויה לא תקין עבור הפריט " + ((TextBlock)Items_Table.Children[15 + (i - 1) * 4 - 2]).Text + "\n";
                    correctAmounts = false;
                }
                else if(t.Text != "")
                {
                    TextBlock b = (TextBlock)Items_Table.Children[15 + (i-1) * 4];
                    TextBlock cAmnt = (TextBlock)Items_Table.Children[15 + (i - 1) * 4 -1];
                    int wantedAmount, currentAmount;
                    wantedAmount = Convert.ToInt32(t.Text);
                    currentAmount = Convert.ToInt32(cAmnt.Text);
                    SoldItems.Add(((TextBlock)Items_Table.Children[15 + (i - 1) * 4 - 3]).Text,wantedAmount);
                    if(wantedAmount > currentAmount)
                    {
                        correctAmounts = false;
                        errMsg += "קיימת חריגה בכמות עבור הפריט: " + ((TextBlock)Items_Table.Children[15 + (i - 1) * 4 - 2]).Text +"\n";
                    }
                    total += Convert.ToInt32(t.Text) * Convert.ToDouble(b.Text);
                }
            }
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
