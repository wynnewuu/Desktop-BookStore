using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    class FavoritingData
    {
        SqlConnection conn;
        DataSet dsBooks;
        public FavoritingData()
        {
            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
        }
        public DataSet GetFavorites(int userID)
        {
            try
            {
                String strSQL = "SELECT * FROM BookData as B JOIN Favorite_On as F ON B.ISBN = F.ISBN WHERE UserId = " + userID.ToString() + ";";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Books");            //Get book info 

                /* Console.WriteLine(dsBooks.Tables["Books"].Rows.Count);
                 Debug.WriteLine(dsBooks.Tables);

                 Console.WriteLine("HELLO");
                 Debug.WriteLine("HELLO");*/
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsBooks;
        }
    }
}
