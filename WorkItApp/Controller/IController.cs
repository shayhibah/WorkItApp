using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItApp.Model;
using WorkItApp.View;

namespace WorkItApp.Controller
{
    public interface IController
    {
         void setModel(IModel m);
         void setView(IView v);
        string Add_New_Cust(string ID, string First_Name, string Last_Name, string Address, string Phone, string Date_of_Birth, string Email, string Password);
        string searchBonusCust(string id);
        List<string> getCurrentBonuses();
        string addBonusToCust(string currentID, int v);
        List<string> getItems();
        string searchItemCust(string text);
        string updateAmounts(Dictionary<string, int> soldItems, string text, double total);
        List<object> calculateItemsSale(Items_Sale_Page items_Sale_Page);
    }
}
