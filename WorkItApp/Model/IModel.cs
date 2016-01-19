using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkItApp.Model
{
     public interface IModel
    {
        string Add_New_Cust(string ID, string First_Name, string Last_Name, string Address, string Phone, string Date_of_Birth, string Email, string Password);
        string searchBonusCust(string id);
        List<string> getCurrentBonuses();
        string addBonusToCust(string currentID, int bonus_number);
        List<string> getItems();
        string searchItemCust(string id);
        string updateSoldItems(Dictionary<string, int> soldItems, string cust_id, double total);
    }
}
