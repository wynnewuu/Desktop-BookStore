using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace BookStoreLIB {
  class DALReviewInfo {
    SqlConnection conn;
    DataSet dsReviews;
    public DALReviewInfo() {
      conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
    }
    public DataSet FindReview(string bookId) {      
      try {
        dsReviews = new DataSet("Reviews");
        String strSQL = "Select USERID, REVIEW from Review where ISBN = " + bookId + " ";
        SqlCommand cmdSelReview = new SqlCommand(strSQL, conn);
        SqlDataAdapter daReview = new SqlDataAdapter(cmdSelReview);        
        daReview.Fill(dsReviews, "Reviews");        
      } catch (Exception ex) { Debug.WriteLine(ex.Message); }            
      return dsReviews;
    }

        
    public int AddReview(int userId, string bookId, string bookTitle, string review) {   
          var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
          try {
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into REVIEW(USERID,ISBN, TITLE, REVIEW) values(@userid, @bookid, @title, @review);";
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@bookid", bookId);
            cmd.Parameters.AddWithValue("@title", bookTitle);
            cmd.Parameters.AddWithValue("@review", review);
        //return 0;

            int result = cmd.ExecuteNonQuery();
            if (result > 0) return result;
            else return -1;

          } catch (Exception ex) {
            Debug.WriteLine(ex.ToString());
            return -1;
          } finally {
            if (conn.State == ConnectionState.Open)
              conn.Close();
          }

    }

    /*
    public int removeFavorite(int userid, object isbn) {
      var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
      try {

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "delete from favorite_on where userid=@userid AND isbn=@isbn;";
        conn.Open();
        cmd.Parameters.AddWithValue("@userid", userid);
        cmd.Parameters.AddWithValue("@isbn", isbn);

        //return 0;

        int result = cmd.ExecuteNonQuery();
        if (result >= 0) return result;
        else return -1;

      } catch (Exception ex) {
        Debug.WriteLine(ex.ToString());
        return -1;
      } finally {
        if (conn.State == ConnectionState.Open)
          conn.Close();
      }
    }

    public int addFavorite(int userid, string isbn) {
      var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
      try {

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "" +
            "BEGIN" +
            "   IF NOT EXISTS (SELECT * FROM favorite_on Where userid=@userid AND isbn=@isbn)" +
            "   BEGIN" +
            "       insert into favorite_on(userid, isbn) VALUES(@userid, @isbn);" +
            "   END;" +
            "END;";
        conn.Open();
        cmd.Parameters.AddWithValue("@userid", userid);
        cmd.Parameters.AddWithValue("@isbn", isbn);

        //return 0;

        int result = cmd.ExecuteNonQuery();
        if (result >= 0) return result;
        else return -1;

      } catch (Exception ex) {
        Debug.WriteLine(ex.ToString());
        return -1;
      } finally {
        if (conn.State == ConnectionState.Open)
          conn.Close();
      }
    }

    public int removeUser(string inputName)  // For test purposes only. Not for application use
    {
      var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
      try {

        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "delete from userData where UserName=@userid;";
        conn.Open();
        cmd.Parameters.AddWithValue("@userid", inputName);

        //return 0;

        int result = cmd.ExecuteNonQuery();
        if (result > 0) return result;
        else return -1;

      } catch (Exception ex) {
        Debug.WriteLine(ex.ToString());
        return -1;
      } finally {
        if (conn.State == ConnectionState.Open)
          conn.Close();
      }
    }

    public Boolean isManager(string userName, string password) {
      var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
      try {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT UserID FROM UserData WHERE Manager = 1 and UserName=@UserName";
        cmd.Parameters.AddWithValue("@UserName", userName);
        //cmd.Parameters.AddWithValue("@Password", password);
        conn.Open();
        int? userId = (int?)cmd.ExecuteScalar();
        if (userId > 0) return true;
        else return false;
      } catch (Exception ex) {
        Debug.WriteLine(ex.ToString());
        return false;
      } finally {
        if (conn.State == ConnectionState.Open)
          conn.Close();
      }
    }
    */
  }
}