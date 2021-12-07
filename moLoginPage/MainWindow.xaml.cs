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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {

        DataSet dsFavorites;
        DataSet dsBookCat;
        UserData userData;
        BookOrder bookOrder;
        PromoData promodata;
        ReviewData reviewdata;
        FavoritesData favorites;

        public MainWindow() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // populate categories and books
            bookOrder = new BookOrder();
            userData = new UserData();
            promodata = new PromoData();

            // populate categories and books
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            this.DataContext = dsBookCat.Tables["Category"];

            this.orderListView.ItemsSource = bookOrder.OrderItemList;
            // disable buttons
            Button_IsEnabled(false);
            Manager_Button_IsEnabled(false);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                if (userData.LogIn(dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
                {
                   
                    this.statusTextBlock.Text += "You are logged in as " + dlg.nameTextBox.Text;
                    MessageBox.Show(this.statusTextBlock.Text);
                    // enable buttons
                    Button_IsEnabled(true);
                }
                else
                {
                    this.statusTextBlock.Text = "Your login failed. Please try again.";
                    MessageBox.Show(this.statusTextBlock.Text);
                    this.loginButton_Click(sender, e);
                }
            }
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var rgt = new RegisterDialog();
            rgt.Owner = this;
            rgt.ShowDialog();
            // Process data entered by user if dialog box is accepted
            if (rgt.DialogResult == true)
            {
                if (userData.Register(rgt.username.Text, rgt.password.Password, "RG", (bool)rgt.ismanager.IsChecked, rgt.fullname.Text) == true)
                {
                    this.statusTextBlock.Text = "You are logged in as " + rgt.username.Text;
                    MessageBox.Show(this.statusTextBlock.Text);
                    // enable buttons
                    Button_IsEnabled(true);
                }
                else
                {
                    this.statusTextBlock.Text = "Registration failed. Please try again.";
                    MessageBox.Show(this.statusTextBlock.Text);
                    this.registerButton_Click(sender, e);
                }
            }
        }
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            // pass userData to AccountSettingsDialog
            AccountSettingsDialog settingsDialog = new AccountSettingsDialog(userData, bookOrder.getHistory(userData.getUserID()));
            settingsDialog.Owner = this;
            settingsDialog.ShowDialog();

            DataSet Books = bookOrder.getHistory(userData.getUserID());

            //favorites = new FavoritesData();
            //dsFavorites = favorites.GetFavorites(userData.getUserID());

            //FavoritesDialog dlg = new FavoritesDialog(dsFavorites, userData.getUserID());
            //dlg.ShowDialog();
        }
        
        
        
        
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OrderItemDialog orderItemDialog = new OrderItemDialog();
            DataRowView selectedRow;
            if (this.ProductsDataGrid.SelectedItems.Count != 0)
            {
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                orderItemDialog.Owner = this;
                orderItemDialog.ShowDialog();
                if (orderItemDialog.DialogResult == true)
                {
                    string isbn = orderItemDialog.isbnTextBox.Text;
                    string title = orderItemDialog.titleTextBox.Text;
                    double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                    int quantity = int.Parse(orderItemDialog.quantityTextBox.Text);
                    bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));
                }
            }
            else
            {
                MessageBox.Show("Please select a book to add in the cart.");
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                bookOrder.RemoveItem(selectedOrderItem.BookID);
            }
        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {
            int orderId;
            orderId = bookOrder.PlaceOrder(userData.UserID);
            if (bookOrder != null)
            {
                double total = bookOrder.GetOrderTotal() * (1 - (0.01 * promodata.Percentage));
                MessageBox.Show("Your order has been placed. Your order id is " +
           orderId.ToString() + ", Your order total is " + Math.Round(total, 2));
            }
            else
            {
                MessageBox.Show("Your cart is empty");
            }
        }

        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
            FavoriteItemDialog favoriteItemDialog = new FavoriteItemDialog();
            DataRowView selectedRow;
            if (this.ProductsDataGrid.SelectedItems.Count != 0)
            {
                try
                {
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                    favoriteItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                    favoriteItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                    favoriteItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                    favoriteItemDialog.Owner = this;
                    favoriteItemDialog.ShowDialog();
                    if (favoriteItemDialog.DialogResult == true)
                    {
                        if (userData.isLogged())
                        {
                            string isbn = favoriteItemDialog.isbnTextBox.Text;
                            if (userData.addFavorite(isbn))
                            {
                                MessageBox.Show("This book has been added to your Favorites");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please log in to Favorite a Book");
                        }
                    }
                }
                catch (System.Exception exp)
                {
                    //MessageBox.Show(exp.Message);
                    Debug.WriteLine(exp.GetType());
                }
            }
            else
            {
                MessageBox.Show("Please select a book to add in the cart.");
            }
        }
        private void AddtoWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            WishlistDialog WishlistDialog = new WishlistDialog();
            DataRowView selectedRow;

            try
            {
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];

                WishlistDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                WishlistDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                WishlistDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                WishlistDialog.yearTextBox.Text = selectedRow.Row.ItemArray[5].ToString();
                WishlistDialog.authorTextBox.Text = selectedRow.Row.ItemArray[3].ToString();
                WishlistDialog.Owner = this;
                WishlistDialog.ShowDialog();

                if (WishlistDialog.DialogResult == true)
                {
                    string title = WishlistDialog.titleTextBox.Text;
                    string isbn = WishlistDialog.isbnTextBox.Text;
                    string price = WishlistDialog.priceTextBox.Text;
                    string year = WishlistDialog.yearTextBox.Text;
                    string author = WishlistDialog.authorTextBox.Text;

                    if (userData.AddtoWishlist(title, isbn, price, year, author))
                    {
                        MessageBox.Show("This book is now in your Wishlist!");
                    }
                }
            }

            catch (Exception exp)
            {
                MessageBox.Show("This book could not be added to your Wishlist.");
                Debug.WriteLine(exp.GetType());
            }
        }

        private void ViewWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            WishlistView WishlistView = new WishlistView(userData);
            WishlistView.Owner = this;
            WishlistView.ShowDialog();

        }
        private void seeFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            if (userData.isLogged()) {
                // Load Favorites Data Regardless of user
                favorites = new FavoritesData();
                dsFavorites = favorites.GetFavorites(userData.getUserID());

                FavoritesDialog dlg = new FavoritesDialog(dsFavorites, userData.getUserID());
                dlg.ShowDialog();
            }
        }

        private void PromoButton_Click(object sender, RoutedEventArgs e)
        {
            PromocodeDialog dlg = new PromocodeDialog();
            // multi applying promo code is not allowed
            if (promodata.isApplied())
            {
                this.statusTextBlock.Text = "You already applied.";
                MessageBox.Show(this.statusTextBlock.Text);
            }
            else
            {
                dlg.ShowDialog();
                if (dlg.DialogResult == true)
                {
                    if (promodata.Apply(dlg.codeTextBox.Text))
                    {
                        this.statusTextBlock.Text = "Code succesfully applied!";
                        MessageBox.Show(this.statusTextBlock.Text);

                    }
                    else
                    {
                        this.statusTextBlock.Text = "Invalid promotion code. Please try again.";
                        MessageBox.Show(this.statusTextBlock.Text);
                        this.PromoButton_Click(sender, e);
                    }
                }
            }
        }
        private void reviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedIndex >= 0)
            {
                DataRowView selectedRow;
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                string m_isbn = selectedRow.Row.ItemArray[0].ToString();
                //this.DataContext = dsReviews.Tables["Review"];

                ReviewDialog reviewDialog = new ReviewDialog();
                //Debug.WriteLine("m_isbn=");
                //Debug.WriteLine(m_isbn);
                reviewDialog.ISBN = m_isbn;
                reviewDialog.show();
                reviewDialog.isbnTextBox.Text = m_isbn;
                reviewDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                reviewDialog.Owner = this;
                reviewDialog.ShowDialog();

                if (reviewDialog.DialogResult == true)
                {
                    string isbn = reviewDialog.isbnTextBox.Text;
                    string title = reviewDialog.titleTextBox.Text;
                    string review = reviewDialog.submitTextBox.Text;
                    reviewdata.AddReview(userData.UserID, isbn, title, review);
                }
            }
            else
            {
                MessageBox.Show("Please make a selection before giving a review", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void ProductsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void insightButton_Click(object sender, RoutedEventArgs e) 
        {

            var dlg = new InsightsWindowDialog();
            
            //dlg.PurchaseHistoryDataGrid.ItemsSource = new PurchaseHistoryData().getTopSellingBooks();
            dlg.ShowDialog();
        }

        /// <summary>
        /// Method for showing the book manage panel
        /// </summary>
        private void BookManageButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerWorkSpace mws = new ManagerWorkSpace();
            mws.Owner = this;
            mws.ShowDialog();
        }
        /// <summary>
        /// Method for showing the user manage panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserManageButton_Click(object sender, RoutedEventArgs e)
        {
            UserInfoAdmin uia = new UserInfoAdmin();
            uia.Owner = this;
            uia.ShowDialog();
        }

        /// <summary>
        /// Method for managers to login through Admin button
        /// </summary>
        private void ManagerloginButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerLoginDialog mld = new ManagerLoginDialog();
            mld.Owner = this;
            mld.ShowDialog();

            if (mld.DialogResult == true)
            {
                if (userData.LogIn(mld.manager_nameTextBox.Text, mld.manager_passwordTextBox.Password) == true)
                {
                    if (userData.isManager)
                    {
                        this.statusTextBlock.Text = "Manager: ";
                    }
                    this.statusTextBlock.Text += "You are logged in as " + mld.manager_nameTextBox.Text;
                    MessageBox.Show(this.statusTextBlock.Text);
                    // enable buttons for admin
                    Manager_Button_IsEnabled(true);
                }
                else
                {
                    this.statusTextBlock.Text = "Your login failed. Please try again.";
                    MessageBox.Show(this.statusTextBlock.Text);
                    this.ManagerloginButton_Click(sender, e);
                }
            }
        }

       

        /// <summary>
        /// Button controller
        /// </summary>
        /// <param name="var"></param>
        private void Button_IsEnabled(bool var)
        {
            AddtoWishlistButton.IsEnabled = var;
            ViewWishlistButton.IsEnabled = var;
            addButton.IsEnabled = var;
            favoriteButton.IsEnabled = var;
            seeFavoriteButton.IsEnabled = var;
            checkoutOrderButton.IsEnabled = var;
            PromoButton.IsEnabled = var;
            reviewButton.IsEnabled = var;
            RemoveButton.IsEnabled = var;
            settingsButton.IsEnabled = var;
        }

        /// <summary>
        /// (Manager) Button controller
        /// </summary>
        /// <param name="var"></param>
        private void Manager_Button_IsEnabled(bool var)
        {
            Button_IsEnabled(var);
            BookManageButton.IsEnabled = var;
            UserManageButton.IsEnabled = var;
        }

        /// <summary>
        /// Exit method
        /// </summary>
        private void exitButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        
    }
    
}
