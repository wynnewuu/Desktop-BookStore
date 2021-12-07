using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BookStoreLIB
{
    class DALPurchaseHistory
    {
        SqlConnection conn;
        DataTable dsTopSellingBooks;
        public DALPurchaseHistory()
        {
            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
        }


        /// <summary>List top selling books in book store</summary>
        /// <returns>Dataset contains top 5 selling books in book store</returns>
        public DataTable ListTopSellingBooks() {
            
            try
            {
                String strSQL = "Select Title, SUM(Quantity) as 'Books Sold' "
                                + "FROM OrderItem "
                                + "INNER JOIN BookData ON(OrderItem.ISBN = BookData.ISBN) "
                                + "GROUP BY BookData.Title "
                                + "ORDER BY SUM(Quantity) DESC ";
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                SqlDataAdapter daTopSellingBooks = new SqlDataAdapter(cmd);

                dsTopSellingBooks = new DataTable();
                daTopSellingBooks.Fill(dsTopSellingBooks);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }

            return dsTopSellingBooks;
        }
    }
}
