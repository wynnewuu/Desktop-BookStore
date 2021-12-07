using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace BookStoreLIB
{
    internal class DALPromoCode
    {
        public int Apply(string code)
        {
            var conn = new SqlConnection(Properties.Settings.Default.moConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select Percentage from Promocode where "
                    + "Code = @Code";
                cmd.Parameters.AddWithValue("@Code", code);
                conn.Open();
                int? percentage = (int?)cmd.ExecuteScalar();
                if (percentage > 0)
                {
                    return (int)percentage;
                }       
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
