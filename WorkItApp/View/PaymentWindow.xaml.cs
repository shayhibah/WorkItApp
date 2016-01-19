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
using System.Windows.Shapes;

namespace WorkItApp.View
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private bool answer = false;
        public PaymentWindow()
        {
            InitializeComponent();
        }

        public bool getStatus()
        {
            return answer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            answer = false;
            bool ans=false;
            bool validValues = true;
            long n = 0; int m = 0;
            string errMsg = "";
            if (!long.TryParse(this.creditCardNumberBox.Text, out n))
            {
                errMsg += "מספר כרטיס אשראי לא תקין\n";
                validValues = false;
            }
            if (!int.TryParse(this.securityCodeBox.Text, out m) || m.ToString().Length<3 || m.ToString().Length > 4 || m<0)
            {
                errMsg += "קוד ביטחון שגוי\n";
                validValues = false;
            }
            if (!int.TryParse(this.monthBox.Text, out m) || m < 1 || m > 12)
            {
                errMsg += "החודש שהוזן איננו תקין\n";
                validValues = false;
            }
            if (!int.TryParse(this.yearBox.Text, out m) || m < DateTime.Now.Year || m > DateTime.Now.Year+10)
            {
                errMsg += "השנה שהוזנה איננה תקינה\n";
                validValues = false;
            }
            if (!validValues)
                MessageBox.Show(errMsg);
            while (!ans && validValues)
            {
                // this.Close();
                Thread.Sleep(1500);
                Random r = new Random();
                int num = r.Next(0, 100);
                if (num > 20)
                {
                    MessageBox.Show("Transaction was done successfully!");
                    ans = true;
                    this.Close();
                    answer = true;
                }
                else
                {
                    MessageBox.Show("An error occurred during processing. Please try again.");
                    break;
                }
            }

            
        }

    }
}
