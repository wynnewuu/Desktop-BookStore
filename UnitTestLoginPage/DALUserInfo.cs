using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace BookStoreLIB
{
    internal class DALUserInfo
    {
        public int LogIn(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select UserID from UserData where "
                    + "UserName = @UserName and Password = @Password";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                int? userId = (int?)cmd.ExecuteScalar();
                if (userId > 0) return (int)userId;
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

        public int Register(string Username, string Password, string Type, Boolean isManager, string fullName)
        {
            if (Username != "" && Password != "")
            {
                // at least 6 characters
                var atLeast6Characters = new Regex(@".{6,}");
                // contains letters
                var letters = new Regex(@"\w");
                // contain numbers
                var numbers = new Regex(@"\d");
                // must start with a letter
                var startWithLetter = new Regex(@"^[a-zA-Z].*");
                // no special characters
                var special = new Regex(@"[a-zA-Z0-9]+$");
                // check if entered password is valid
                var valid = atLeast6Characters.IsMatch(Password) && letters.IsMatch(Password) && numbers.IsMatch(Password) && startWithLetter.IsMatch(Password) && special.IsMatch(Password);
                if (valid)
                {

                    var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select top 1 UserID from UserData order by userid desc;";
                        conn.Open();
                        int? lastId = (int?)cmd.ExecuteScalar();

                        cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into UserData(userid, username, password, type, Manager, FullName) values(@userId, @Username, @Password, @Type, @isManager, @fullName);";
                        cmd.Parameters.AddWithValue("@userId", (lastId + 1));
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Type", Type);
                        cmd.Parameters.AddWithValue("@isManager", (isManager ? 1 : 0));
                        cmd.Parameters.AddWithValue("@fullName", fullName);

                        //return 0;

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0) return result;
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
                else
                {
                    // MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
                    Debug.WriteLine("A valid password needs to have at least six characters with both letters and numbers.");
                    return -1;
                }
            }
            else
            {
                // MessageBox.Show("Please fill in all slots.");
                Debug.WriteLine("Please fill in all slots.");
                return -1;
            }

        }

        public int removeFavorite(int userid, object isbn)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {

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

        public int addFavorite(int userid, string isbn)
        { 
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {

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

        public int AddtoWishlist(int userid, string title, string isbn, string price, string year, string author)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "BEGIN IF NOT EXISTS (SELECT * FROM Wishlist Where userid=@userid AND isbn=@isbn)" +
                                  "BEGIN insert into Wishlist(isbn, userid, title, price, year, author) VALUES(@isbn, @userid, @title, @price, @year, @author);" + "END;" + "END;";
                conn.Open();
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@author", author);

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

        public int RemovefromWishlist(int userid, object isbn)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from Wishlist where Userid=@userid AND ISBN=@isbn;";
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

        public int changeUsername(int userid, string username)
        {
            if (username != "")
            {
                var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE UserData SET UserName=@username WHERE UserID=@userid;";
                    conn.Open();
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@userid", userid);

                    //return 0;

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
            else
            {
                Debug.WriteLine("Username cannot be empty");
                return -1;
            }
        }
        public int changePassword(int userid, string password)
        {
            if (password != "")
            {
                // at least 6 characters
                var atLeast6Characters = new Regex(@".{6,}");
                // contains letters
                var letters = new Regex(@"\w");
                // contain numbers
                var numbers = new Regex(@"\d");
                // must start with a letter
                var startWithLetter = new Regex(@"^[a-zA-Z].*");
                // no special characters
                var special = new Regex(@"[a-zA-Z0-9]+$");
                // check if entered password is valid
                var valid = atLeast6Characters.IsMatch(password) && letters.IsMatch(password) && numbers.IsMatch(password) && startWithLetter.IsMatch(password) && special.IsMatch(password);
                if (valid)
                {
                    var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE UserData SET Password=@password WHERE UserID=@userid;";
                        conn.Open();
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@userid", userid);

                        //return 0;

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
                else
                {
                    Debug.WriteLine("A valid password needs to have at least six characters with both letters and numbers.");
                    return -1;
                }
            }
            else
            {
                Debug.WriteLine("Password cannot be empty");
                return -1;
            }
        }
        public int changeFullName(int userid, string fullName)
        {
            if (fullName != "")
            {
                var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE UserData SET FullName=@fullName WHERE UserID=@userid;";
                    conn.Open();
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@userid", userid);

                    //return 0;

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
            else
            {
                Debug.WriteLine("Full Name cannot be empty");
                return -1;
            }
        }
        public int deleteAccount(int userid)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from userData where UserID=@userid;";
                conn.Open();
                cmd.Parameters.AddWithValue("@userid", userid);

                //return 0;

                int result = cmd.ExecuteNonQuery();
                if (result > 0) return result;
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

        public Boolean isManager(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT UserID FROM UserData WHERE Manager = 1 and UserName=@UserName";
                cmd.Parameters.AddWithValue("@UserName", userName);
                //cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                int? userId = (int?)cmd.ExecuteScalar();
                if (userId > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}