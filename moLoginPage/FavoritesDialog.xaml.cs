using BookStoreLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for FavoritesDialog.xaml
    /// </summary>
    public partial class FavoritesDialog : Window
    {
        int UserId;
        public FavoritesDialog(DataSet dataSet, int UserId)
        {
            this.UserId = UserId;

            this.DataContext = dataSet.Tables["Books"];
            InitializeComponent();
        }

        private void removeFavorite(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow;
            if (this.Favorites.SelectedItems.Count != 0)
            {
                try
                {
                    selectedRow = (DataRowView)this.Favorites.SelectedItems[0];
                    selectedRow.Row.ItemArray[0].ToString();
                    string isbn = selectedRow.Row.ItemArray[0].ToString();

                    Console.WriteLine(this.UserId);

                    FavoritesData favorites = new FavoritesData();

                    favorites.removeFavorite(this.UserId, isbn);

                    this.DataContext = favorites.GetFavorites(this.UserId).Tables["Books"];

                }
                catch (System.Exception exp)
                {
                    //MessageBox.Show(exp.Message);
                    Debug.WriteLine(exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select a favorite to remove.");
            }
        }

        private void Favorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
