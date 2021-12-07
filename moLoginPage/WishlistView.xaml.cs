using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using BookStoreLIB;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookStoreGUI
{
    public partial class WishlistView : Window
    {
        DataTable dsWishl;
        UserData userData;
        public WishlistView(UserData mainUserData)
        {
            userData = mainUserData;
            InitializeComponent();
            int userid = userData.getUserID();
            CallWishlist(userid);
        }

        private void CallWishlist(int userid)
        {
            Wishlist Wishl = new Wishlist();
            dsWishl = Wishl.GetWishlistInfo(userid);
            WishlistDataGrid.ItemsSource = dsWishl.DefaultView;
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

            private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow;
            int userid = userData.getUserID();
            try
            {
                selectedRow = (DataRowView)this.WishlistDataGrid.SelectedItems[0];
                string isbn = selectedRow.Row.ItemArray[1].ToString();
                if (userData.RemovefromWishlist(isbn))
                    {
                        MessageBox.Show("This book is now removed from your Wishlist.");
                        CallWishlist(userid);
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show("This book could not be removed to your Wishlist.");
                Debug.WriteLine(exp.GetType());
            }
        }

        private void WishlistDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
