using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkItApp.Controller;
using System.Data.OleDb;
using System.IO;
using WorkItApp.View;

namespace WorkItApp.Model
{
    public class MyModel : IModel
    {
        private IController controller;
        private OleDbConnection connection = new OleDbConnection();
        private List<string> bonuses;

        public MyModel(IController controller)
        {
            this.controller = controller;
            this.bonuses = new List<string>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + @"\WorkIt.accdb;Persist Security Info=False;";
        }

        public List<string> getCurrentBonuses()
        {
            return bonuses;
        }

        public string Add_New_Cust(string ID, string First_Name, string Last_Name, string Address, string Phone, string Date_of_Birth, string Email, string Password)
        {
            string result = "";

            connection.Open();
            OleDbCommand searchCommand = new OleDbCommand();
            searchCommand.Connection = connection;
            searchCommand.CommandText = "select Customer_ID from Customers where Customer_ID='" + ID + "'";
            OleDbDataReader reader = searchCommand.ExecuteReader();
            if (reader.Read() == false) //No Matching ID
            {
                PaymentWindow payment_window = new PaymentWindow();
                payment_window.ShowDialog();
                if (payment_window.getStatus())
                {
                    OleDbCommand addCommand = new OleDbCommand();
                    addCommand.Connection = connection;
                    addCommand.CommandText = "insert into Customers (Customer_ID, First_Name, Last_Name, Address, Phone, Date_Of_Birth, Email, [Password]) Values ('" + ID + "','" + First_Name + "','" + Last_Name + "','" + Address + "','" + Phone + "','" + Date_of_Birth + "','" + Email + "','" + Password + "');";
                    addCommand.ExecuteNonQuery();
                }
            }
            else
                result = "קיים כבר מנוי בעל תעודת זהות זהה";
            connection.Close();
            return result;
        }
        public string searchBonusCust(string ID)
        {
            connection.Open();
            OleDbCommand searchCommand = new OleDbCommand();
            searchCommand.Connection = connection;
            searchCommand.CommandText = "select Customer_ID from Customers where Customer_ID='" + ID + "'";
            OleDbDataReader reader = searchCommand.ExecuteReader();
            if (reader.Read() == false)//No Matching ID
            {
                connection.Close();
                return "לא קיים במערכת מנוי בעל תעודת זהות זו";
            }
            else
            {
                OleDbCommand searchCommand2 = new OleDbCommand(); //Search Cust In Benefits Table
                searchCommand2.Connection = connection;
                searchCommand2.CommandText = "select * from Bonus_Per_Customer where Customer_ID='" + ID + "'";
                OleDbDataReader reader2 = searchCommand2.ExecuteReader();
                if (reader2.Read() == false) //לשלוף הטבות
                {
                    bonuses = getBonuses();
                    connection.Close();
                    return "";
                }
                else //לבדוק תאריך תוקף
                {
                    string cust_end_date = (string)reader2.GetValue(3);
                    DateTime endDateTime = Convert.ToDateTime(cust_end_date);
                    if (endDateTime < DateTime.Now) //לשלוף הטבות
                    {
                        bonuses = getBonuses();
                        connection.Close();
                        return "";
                    }
                    else
                    {
                        connection.Close();
                        return "ללקוח קיימת הטבה פעילה";
                    }
                }
            }
        }

        private List<string> getBonuses()
        {
            List<string> bonuses = new List<string>();
            OleDbCommand searchCommand3 = new OleDbCommand(); //Search Cust In Benefits Table
            searchCommand3.Connection = connection;
            searchCommand3.CommandText = "select * from Bonuses";
            OleDbDataReader reader3 = searchCommand3.ExecuteReader();
            while (reader3.Read())
            {
                bonuses.Add(Convert.ToString((string)reader3.GetValue(2)) + "\t" + (string)reader3.GetValue(1) + "\t" + reader3.GetValue(0));
            }
            return bonuses;
        }

        public string addBonusToCust(string currentID, int bonus_number)
        {
            connection.Open();
            OleDbCommand insertCommand = new OleDbCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = "insert into Bonus_Per_Customer (Customer_ID, Bonus_ID, Start_Date, End_Date) Values ('" + currentID + "','" + bonus_number + "','" + DateTime.Now.Date.ToShortDateString() + "','" + DateTime.Now.AddYears(1).Date.ToShortDateString() + "');";
            insertCommand.ExecuteNonQuery();
            connection.Close();
            return "ההטבה נוספה ללקוח בהצלחה";
        }

        public List<string> getItems()
        {
            List<string> items = new List<string>();
            connection.Open();
            OleDbCommand searchCommand = new OleDbCommand(); //Search Cust In Benefits Table
            searchCommand.Connection = connection;
            searchCommand.CommandText = "select * from Items";
            OleDbDataReader reader = searchCommand.ExecuteReader();
            while (reader.Read())
            {
                items.Add(Convert.ToString(reader.GetValue(0)) + "\t" + (string)reader.GetValue(1) + "\t" + Convert.ToInt32(reader.GetValue(2)) + "\t" + Convert.ToDouble(reader.GetValue(3)));
            }
            connection.Close();
            return items;
        }

        public string searchItemCust(string ID)
        {
            connection.Open();
            OleDbCommand searchCommand = new OleDbCommand();
            searchCommand.Connection = connection;
            searchCommand.CommandText = "select Customer_ID from Customers where Customer_ID='" + ID + "'";
            OleDbDataReader reader = searchCommand.ExecuteReader();
            if (reader.Read() == false)//No Matching ID
            {
                connection.Close();
                return "לא קיים במערכת מנוי בעל תעודת זהות זו";
            }
            else
            {
                connection.Close();
                return "";
            }
        }

        public string updateSoldItems(Dictionary<string, int> soldItems, string cust_id, double total)
        {
            Random r = new Random();
            int order_num = r.Next(1000, 9999);
            connection.Open();
            OleDbCommand searchCommand = new OleDbCommand(); //Make sure that the order num isnt exist
            OleDbCommand updateCommand = new OleDbCommand(); //update amounts
            searchCommand.Connection = connection;
            updateCommand.Connection = connection;
            searchCommand.CommandText = "select Order_ID from Orders where Order_ID='" + order_num + "'";
            OleDbDataReader reader = searchCommand.ExecuteReader();
            while (reader.Read() == true)
            {
                order_num = r.Next(1000, 9999);
                searchCommand.CommandText = "select Order_ID from Orders where Order_ID='" + order_num + "'";
                reader = searchCommand.ExecuteReader();
            }
            updateCommand.CommandText = "insert into Orders (Order_ID, Customer_ID, Order_Date, Total_Sum) Values ('" + order_num + "','" + cust_id + "','" + DateTime.Now.Date.ToShortDateString() + "','" + total + "');";
            updateCommand.ExecuteNonQuery();
            foreach(KeyValuePair<string,int> pair in soldItems)
            {
                updateCommand.CommandText = "insert into Items_Per_Order (Item_ID, Order_ID, [Count]) Values ('" + Convert.ToInt32(pair.Key) + "','" + Convert.ToString(order_num) + "','" +  pair.Value + "');";
                updateCommand.ExecuteNonQuery();
            }
            foreach(KeyValuePair<string,int> pair in soldItems)
            {
                OleDbCommand searchCommand2 = new OleDbCommand(); //Make sure that the order num isnt exist
                searchCommand2.Connection = connection;
                searchCommand2.CommandText = "select * from Items where Item_ID=" + Convert.ToInt32(pair.Key) + "";
                OleDbDataReader reader2 = searchCommand2.ExecuteReader();
                reader2.Read();
                int currentAmount = (int)reader2.GetValue(2);
                int newAmount = currentAmount - pair.Value;
                updateCommand.CommandText = "update [Items] set [Count]= ? where Item_ID= ?";
                updateCommand.Parameters.AddWithValue("Count", newAmount);
                updateCommand.Parameters.AddWithValue("Item_ID", Convert.ToInt32(pair.Key));
                updateCommand.ExecuteNonQuery();
            }
            connection.Close();

            return Convert.ToString(order_num);
        }
    }
}