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
    public class FavoritesData
    {
        SqlConnection conn;
        DataSet dsBooks;
        public FavoritesData()
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
        public int removeFavorite(int userid, string isbn)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "" +
                    "BEGIN" +
                    "   IF EXISTS (SELECT * FROM favorite_on Where userid=@userid AND isbn=@isbn)" +
                    "   BEGIN" +
                    "       delete from favorite_on WHERE userid=@userid AND isbn=@isbn;" +
                    "   END;" +
                    "END;";
                conn.Open();
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                int result = cmd.ExecuteNonQuery();
                if (result >= 0) return result;
                else return -1;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
