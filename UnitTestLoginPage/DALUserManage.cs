using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB
{
    internal class DALUserManage
    {
        private SqlConnection conn;


        /// <summary>
        /// for building the connection to database
        /// </summary>
        public DALUserManage()
        {
            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);

        }



        /// <summary>
        /// method for adding new user information into the table UserData in the database
        /// </summary>
       
        public int AddUserIntoDataBase(int userid, string username, string password, string type, Boolean isManager,string fullname)
        {
            int RowsAffected = 0;
            try
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO UserData" +
                    "(UserID,UserName,Password,Type,Manager,FullName)" +
                    "VALUES(@userid,@username,@password,@type,@isManager,@fullname)", conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@isManager", isManager);
                cmd.Parameters.AddWithValue("@fullname", fullname);
                

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
        /// method for deleting user information from the table UserData in the database
        /// </summary>
        /// <returns></returns>
        public int DeleteUserFromDataBase(int userid)
        {
            int RowsAffected = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from UserData where UserID = @userid", conn);
                cmd.Parameters.AddWithValue("@userid", userid);

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
        /// Method for getting a data row from userdata table and return the data through a UserAdmin object 
        /// </summary>
        /// <returns></returns>
        public UserAdmin GetUserFromDataBase(int userid)
        {
            UserAdmin userInfo = new UserAdmin(userid);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from UserData where UserID = @userid", conn);
                cmd.Parameters.AddWithValue("@userid", userid);

                conn.Open();
                //------------------------------------------------------------------------
                //read the data row from book table and store them into a UserAdmin object 
                //------------------------------------------------------------------------
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    userInfo.username = reader[1].ToString();
                    userInfo.password = reader[2].ToString();
                    userInfo.type = reader[3].ToString();
                    userInfo.isManager = Convert.ToBoolean(reader[4]);
                    userInfo.fullname = reader[5].ToString();
                    
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
            return userInfo;
        }

    }
}