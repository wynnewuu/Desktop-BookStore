using System;
using System.Collections.Generic;
using System.Data;
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
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for InsightsWindowDialog.xaml
    /// </summary>
    public partial class InsightsWindowDialog : Window
    {
        public DataTable dsTopSellingBooks;
        public InsightsWindowDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(Object sender, RoutedEventArgs e ) 
        {
            var purchaseHistory = new PurchaseHistoryData();
            dsTopSellingBooks =  purchaseHistory.getTopSellingBooks();
            PurchaseHistoryDataGrid.DataContext = dsTopSellingBooks;


        }
    }
}
