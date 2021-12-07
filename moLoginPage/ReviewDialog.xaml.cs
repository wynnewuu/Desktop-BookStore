/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using BookStoreLIB;
using System.Diagnostics;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for OrderItemDialog.xaml
    /// </summary>
    public partial class ReviewDialog : Window
    {
        private string isbn; // field

        public string ISBN   // property
        {
          get { return isbn; }   // get method
          set { isbn = value; }  // set method
        }
    public ReviewDialog()
        {        
            InitializeComponent();
        }
        public void show() {
          ReviewData reviewData = new ReviewData();
          DataSet dsReviews = reviewData.FindReview(isbn);
          ReviewDataGrid.DataContext = dsReviews.Tables["Reviews"];            
          //ReviewDataGrid.ItemsSource = new DataView(dsReviews.Tables["Reviews"]);
          //Debug.WriteLine("ISBN=");
          //Debug.WriteLine(isbn);
        }
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
