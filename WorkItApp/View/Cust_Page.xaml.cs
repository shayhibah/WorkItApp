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
    /// Interaction logic for Cust_Page.xaml
    /// </summary>
    public partial class Cust_Page : Page
    {
        private Customers_Window Cust_Window;
        public Cust_Page(Customers_Window Cust_Window)
        {
            this.Cust_Window = Cust_Window;
            InitializeComponent();
        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            IController controller = MainWindow.getController();
            bool validValues = true;
            long n = 0;
            string errMsg = "";
            if (this.First_Name.Text == "" || !First_Name.Text.All(Char.IsLetter))
            {
                validValues = false;
                errMsg += "שם פרטי שהוזן איננו תקין\n";
            }
            if (this.Last_Name.Text == "" || !Last_Name.Text.All(Char.IsLetter))
            {
                validValues = false;
                errMsg += "שם המשפחה שהוזן איננו תקין\n";
            }
            if (this.ID.Text == "" || !long.TryParse(this.ID.Text, out n) || this.ID.Text.Length != 9)
            {
                validValues = false;
                errMsg += "תעודת הזהות שהוזנה איננה תקינה\n";
            }
            if (this.Phone.Text == "" || !long.TryParse(this.Phone.Text, out n))
            {
                validValues = false;
                errMsg += "מספר הטלפון שהוזן איננו תקין\n";
            }
            if (this.Birth_Date.Text == "" || this.Birth_Date.Text.Split('/').Length != 3)
            {
                validValues = false;
                errMsg += "כתובת הלידה שהוזנה איננה תקינה\n";
            }
            if (this.Email.Text == "" || !this.Email.Text.Contains("@"))
            {
                validValues = false;
                errMsg += "כתובת המייל שהוזנה איננה תקינה\n";
            }
            if (this.Address.Text == "")
            {
                validValues = false;
                errMsg += "כתובת המגורים שהוזנה איננה תקינה\n";
            }
            if (validValues)
            {
                string result = controller.Add_New_Cust(this.ID.Text, this.First_Name.Text, this.Last_Name.Text, this.Address.Text, this.Phone.Text, this.Birth_Date.Text, this.Email.Text, this.Password.Text);
                if (result != "")
                    MessageBox.Show(result);
                else
                    Cust_Window.Back_to_Main_Btn_Click(null, null);
            }
            else
                MessageBox.Show(errMsg);
            
        }
    }
}
