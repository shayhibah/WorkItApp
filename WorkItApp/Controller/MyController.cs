using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorkItApp.Model;
using WorkItApp.View;

namespace WorkItApp.Controller
{
    public class MyController : IController
    {
        private IModel model;
        private IView view;
        private List<string> bonuses;

        public MyController() { }
        public List<string> getCurrentBonuses()
        {
            return bonuses;
        }
        public void setModel(IModel m)
        {
            this.model = m;
        }
        public void setView(IView v)
        {
            this.view = v;
        }
        public string Add_New_Cust(string ID, string First_Name, string Last_Name, string Address, string Phone, string Date_of_Birth, string Email, string Password)
        {
            string result = model.Add_New_Cust(ID, First_Name, Last_Name, Address, Phone, Date_of_Birth, Email, Password); //If Result is "" - It means that there is no cust with that id
            return result;
        }
        public string searchBonusCust(string id)
        {
            string answer = model.searchBonusCust(id);
            if (answer == "")
            {
                bonuses = model.getCurrentBonuses();
            }
            return answer;
        }
        public string searchItemCust(string id)
        {
            string answer = model.searchItemCust(id);
            return answer;
        }
        public string addBonusToCust(string currentID, int bonus_number)
        {
            return model.addBonusToCust(currentID, bonus_number);
        }
        public List<string> getItems()
        {
            return model.getItems();
        }
        public string updateAmounts(Dictionary<string, int> soldItems, string cust_id, double total)
        {
            return model.updateSoldItems(soldItems, cust_id, total);
        }

        public List<object> calculateItemsSale(Items_Sale_Page items_Sale_Page)
        {
            List<Object> results = new List<object>();
            Dictionary<string, int> SoldItems = new Dictionary<string, int>();
            string errMsg = "";
            double total = 0;
            bool correctAmounts = true;
            for (int i = 1; i <= 5; i++) //Indexes of Items inside Grid
            {
                TextBox t = (TextBox)items_Sale_Page.Items_Table.Children[i + 4];
                int num = 0;
                if (t.Text != "" && !int.TryParse(t.Text, out num))
                {
                    errMsg += "ערך של כמות רצויה לא תקין עבור הפריט " + ((TextBlock)items_Sale_Page.Items_Table.Children[15 + (i - 1) * 4 - 2]).Text + "\n";
                    correctAmounts = false;
                }
                else if (t.Text != "")
                {
                    TextBlock b = (TextBlock)items_Sale_Page.Items_Table.Children[15 + (i - 1) * 4];
                    TextBlock cAmnt = (TextBlock)items_Sale_Page.Items_Table.Children[15 + (i - 1) * 4 - 1];
                    int wantedAmount, currentAmount;
                    wantedAmount = Convert.ToInt32(t.Text);
                    currentAmount = Convert.ToInt32(cAmnt.Text);
                    SoldItems.Add(((TextBlock)items_Sale_Page.Items_Table.Children[15 + (i - 1) * 4 - 3]).Text, wantedAmount);
                    if (wantedAmount > currentAmount)
                    {
                        correctAmounts = false;
                        errMsg += "קיימת חריגה בכמות עבור הפריט: " + ((TextBlock)items_Sale_Page.Items_Table.Children[15 + (i - 1) * 4 - 2]).Text + "\n";
                    }
                    total += Convert.ToInt32(t.Text) * Convert.ToDouble(b.Text);
                }
            }
            results.Add(errMsg);
            results.Add(total);
            results.Add(correctAmounts);
            results.Add(SoldItems);
            return results;
        }
    }
}
