using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB
{
    class DALBookManage
    {
        private SqlConnection conn;
        DataSet dsBooks;


        /// <summary>
        /// for building the connection to database
        /// </summary>
        public DALBookManage()
        {
            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);

        }



        /// <summary>
        /// method for adding new book information into the table BookData in the database
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="categoryid"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <param name="supplierid"></param>
        /// <param name="year"></param>
        /// <param name="edition"></param>
        /// <param name="publisher"></param>
        /// <returns></returns>
        public int AddBookIntoDataBase(String isbn, int categoryid, String title, String author, double price, int supplierid, String year, String edition, String publisher)
        {
            int RowsAffected = 0;
            try
            {
                
                SqlCommand cmd = new SqlCommand("INSERT INTO BookData" +
                    "(ISBN,CategoryID,Title,Author,Price,SupplierId,Year,Edition,Publisher)" +
                    "VALUES(@isbn,@categoryid,@title,@author,@price,@supplierid,@year,@edition,@publisher)", conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@categoryid", categoryid);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@supplierid", supplierid);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.Parameters.AddWithValue("@publisher", publisher);

                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();//execute the sql command and return the number of affected rows

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return RowsAffected;
        }




        /// <summary>
        /// method for deleting book information from the table BookData in the database
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public int DeleteBookFromDataBase(String isbn)
        {
            int RowsAffected = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from BookData where ISBN = @isbn", conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
   
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return RowsAffected;
        }
        /// <summary>
        /// Method for getting a data row from book table and return the data through a BookData object 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public BookData GetBookFromDataBase(String isbn)
        {
            BookData bookInfo = new BookData(isbn);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from BookData where ISBN = @isbn", conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                conn.Open();
                //------------------------------------------------------------------------
                //read the data row from book table and store them into a BookData object 
                //------------------------------------------------------------------------
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    bookInfo.categoryid = Convert.ToInt32(reader[1]);
                    bookInfo.title = reader[2].ToString();
                    bookInfo.author = reader[3].ToString();
                    bookInfo.price = Convert.ToDouble(reader[4]);
                    bookInfo.supplierid = Convert.ToInt32(reader[5]);
                    bookInfo.year = reader[6].ToString();
                    bookInfo.edition = reader[7].ToString();
                    bookInfo.publisher = reader[8].ToString();
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally
            {

                if (conn != null)
                { 
                    conn.Close();
                }
            }
            return bookInfo;
        }

    }
}
