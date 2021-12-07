/*BookOrder.cs*/

/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace BookStoreLIB
{
    public class BookOrder
    {
        ObservableCollection<OrderItem> orderItemList = new
            ObservableCollection<OrderItem>();
        DataSet dsBooks;

        public ObservableCollection<OrderItem> OrderItemList
        {
            get { return orderItemList; }
        }
        public void AddItem(OrderItem orderItem)
        {
            foreach (var item in orderItemList)
            {
                if (item.BookID == orderItem.BookID)
                {
                    item.Quantity += orderItem.Quantity;
                    return;
                }
            }
            orderItemList.Add(orderItem);
        }
        public void RemoveItem(string productID)
        {
            foreach (var item in orderItemList)
            {
                if (item.BookID == productID)
                {
                    orderItemList.Remove(item);
                    return;
                }
            }
        }
        public double GetOrderTotal()
        {
            if (orderItemList.Count == 0)
            {
                return 0.00;
            }
            else
            {
                double total = 0;
                foreach (var item in orderItemList)
                {
                    total += item.SubTotal;
                }
                return total;
            }
        }

        public int PlaceOrder(int userID)
        {
            string isbn_data;
            string xmlOrder;
            xmlOrder = "<Order UserID='" + userID.ToString() + "'>";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand();
            cmd.Connection = conn;



            foreach (var item in orderItemList)
            {
                isbn_data = item.ToString();
                xmlOrder += isbn_data;

                isbn_data = (isbn_data.Remove(0, 17));
                isbn_data = (isbn_data.Remove(10));

                Debug.WriteLine(isbn_data);


                try
                {
                    cmd.CommandText = "INSERT INTO History (USERID,ISBN) VALUES (" + userID.ToString() + ",'" + isbn_data + "') ;";
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }

            }
            xmlOrder += "</Order>";
            DALOrder dbOrder = new DALOrder();
            return dbOrder.Proceed2Order(xmlOrder);
        }

        public DataSet getHistory(int userID)
        {

            SqlConnection conn;

            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);

            try
            {

                String strSQL = "SELECT * FROM BookData as B JOIN History as H on B.isbn=H.isbn WHERE userid=" + userID.ToString() + ";";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Books");            //Get book info

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsBooks;
        }
    }
}
