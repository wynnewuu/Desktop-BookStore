/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB {
    class DALWishlist {
        DataTable dt = new DataTable("Wishlist");
        SqlConnection conn;
        public DALWishlist() {
            conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
        }
        public DataTable GetWishlistInfo(int userid) {
            try
            {
                string user = userid.ToString();
                String strSQL = $"Select Title, ISBN, Price, Year, Author from Wishlist where userid = {user}";
                SqlCommand cmdSelList = new SqlCommand(strSQL, conn);
                SqlDataAdapter daList = new SqlDataAdapter(cmdSelList);
                daList.Fill(dt);
            }

            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.Message); 
            }

            return dt;
        }
    }
}