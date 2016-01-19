using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItApp.Model;
using WorkItApp.View;

namespace WorkItApp.Controller
{
    public class MyController : IController
    {
        IModel model;
        IView view;
        List<string> bonuses;
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
    }
}
