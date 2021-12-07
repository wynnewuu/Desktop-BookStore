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
using System.Text.RegularExpressions;
using BookStoreLIB;
namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for ManagerWorkSpace.xaml
    /// </summary>
    public partial class ManagerWorkSpace : Window
    {
        
        public ManagerWorkSpace()
        {
            InitializeComponent();
            editButton.IsEnabled = false;
            
        }

        /// <summary>
        /// Method for add-book button
        /// </summary>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------------
            // varibals store inputs from boxes
            //-----------------------------------------------------
            String isbn = this.rowAddIsbn.Text;

            int categoryid = 0;
            if (this.rowAddCategoryID.Text != "")
            {
                categoryid = int.Parse(this.rowAddCategoryID.Text);
            }

            String title = this.rowAddTitle.Text;
            String author = this.rowAddAuthor.Text;

            double price = 0;
            if (this.rowAddPrice.Text != "")
            {
                price = double.Parse(this.rowAddPrice.Text);
            }

            int supplierid = 0;
            if (this.rowAddSupplierID.Text != "")
            {
                supplierid = int.Parse(this.rowAddSupplierID.Text);
            }

            String year = this.rowAddYear.Text;
            String edition = this.rowAddEdition.Text;
            String publisher = this.rowAddPublisher.Text;
            

            //-----------------------------------------------------
            //call addBook method from BookData class
            //-----------------------------------------------------
            BookData bookdata =new BookData(isbn,categoryid,title,author,price,supplierid,year,edition,publisher);
            
            int flag=bookdata.AddBook();
            if (flag > 0)
            {
                MessageBox.Show($"{flag} book (ISBN:{isbn}) has been added into the store!");
            }
            else { MessageBox.Show($"No book has been added into the store"); }
           
        }

        /// <summary>
        /// Method for delete-book button
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            String isbn = this.rowDeleteThroughIsbn.Text;
            
            //-----------------------------------------------------
            //call DeleteBook method from BookData class
            //-----------------------------------------------------
            BookData bookdata = new BookData(isbn);

            int flag = bookdata.DeleteBook();
            if (flag > 0)
            {
                MessageBox.Show($"{flag} book (ISBN:{isbn}) has been deleted from the store!");
            }
            else { MessageBox.Show($"No book has been deleted from the store"); }
            
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            String isbn = this.editThroughIsbn.Text;
            
            //-----------------------------------------------------
            //call GetBook method from BookData class
            //-----------------------------------------------------
            BookData bookdata = new BookData(isbn);
            
            BookData returnBook = bookdata.GetBook();
            
            if (returnBook.categoryid!=0)
            {
                //-----------------------------------------------------
                //fullfill the boxes with got data
                //-----------------------------------------------------
                editCategoryID.Text=returnBook.categoryid.ToString();
                editTitle.Text = returnBook.title.ToString();
                editAuthor.Text = returnBook.author.ToString();
                editPrice.Text = returnBook.price.ToString();
                editSupplierID.Text = returnBook.supplierid.ToString();
                editYear.Text = returnBook.year.ToString();
                editEdition.Text = returnBook.edition.ToString();
                editPublisher.Text = returnBook.publisher.ToString();

                editButton.IsEnabled = true;//Make the edit button available
            }
            else { MessageBox.Show($"No book has been found from the store"); }
            
        }

        /// <summary>
        /// Edit book information method after getting the information through find button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------------
            //get information from boxes 
            //-----------------------------------------------------
            String isbn = editThroughIsbn.Text;
            int categoryid = Convert.ToInt32(editCategoryID.Text);
            String title = editTitle.Text;
            String author = editAuthor.Text;
            double price = Convert.ToDouble(editPrice.Text);
            int supplierid = Convert.ToInt32(editSupplierID.Text);
            String year = editYear.Text;
            String edition = editEdition.Text;
            String publisher = editPublisher.Text;

            //-----------------------------------------------------
            //delete the original data in the database
            //-----------------------------------------------------
            BookData deleteBook = new BookData(isbn);
            deleteBook.DeleteBook();

            //-----------------------------------------------------
            //Add new data into the database
            //-----------------------------------------------------
            BookData bookdata = new BookData(isbn,categoryid,title,author,price,supplierid,year,edition,publisher);

            int flag = bookdata.AddBook();
            if (flag > 0)
            {
                MessageBox.Show($"book (ISBN:{isbn}) has been edited!");
            }
            else { MessageBox.Show($"No book has been edited."); }

        }

        /// <summary>
        /// Exit button
        /// </summary>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
