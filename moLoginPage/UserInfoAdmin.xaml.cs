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
    /// Interaction logic for UserInfoAdmin.xaml
    /// </summary>
    public partial class UserInfoAdmin : Window
    {
        public UserInfoAdmin()
        {
            InitializeComponent();
            editButton.IsEnabled = false;
        }

        /// <summary>
        /// Method for add-user button
        /// </summary>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------------
            // varibals store inputs from boxes
            //-----------------------------------------------------
            int userid = Convert.ToInt32(this.rowAddUserID.Text);
            String username = this.rowAddUserName.Text;
            String password = this.rowAddPassword.Text;
            String type = this.rowAddType.Text;
            Boolean isManager = Convert.ToBoolean(this.rowAddIfManager.Text);
            String fullname = this.rowAddFullName.Text;
            

           


            //-----------------------------------------------------
            //call AdminAddUser method from UserAdmin class
            //-----------------------------------------------------
            UserAdmin useradmin = new UserAdmin(userid, username, password, type, isManager, fullname);

            int flag = useradmin.AdminAddUser();
            if (flag > 0)
            {
                MessageBox.Show($"{flag} user (USERID:{userid}) has been added !");
            }
            else { MessageBox.Show($"No user has been added "); }

        }

        /// <summary>
        /// Method for delete-user button
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int userid = Convert.ToInt32(this.rowDeleteThroughUserID.Text);

            //-----------------------------------------------------
            //call AdminDeleteUser method from UserAdmin class
            //-----------------------------------------------------
            UserAdmin useradmin = new UserAdmin(userid);

            int flag = useradmin.AdminDeleteUser();
            if (flag > 0)
            {
                MessageBox.Show($"{flag} user (USERID:{userid}) has been deleted !");
            }
            else { MessageBox.Show($"No user has been deleted."); }

        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            int userid = Convert.ToInt32(this.editThroughUserID.Text);

            //-----------------------------------------------------
            //call AdminGetUser method from UserAdmin class
            //-----------------------------------------------------
            UserAdmin useradmin = new UserAdmin(userid);

            UserAdmin returnUser = useradmin.AdminGetUser();

            if (returnUser.username != null)
            {
                //-----------------------------------------------------
                //fullfill the boxes with got data
                //-----------------------------------------------------
                rowEditUserName.Text = returnUser.username.ToString();
                rowEditPassword.Text = returnUser.password.ToString();
                rowEditType.Text = returnUser.type.ToString();
                rowEditIfManager.Text = returnUser.isManager.ToString();
                rowEditFullName.Text = returnUser.fullname.ToString();
                

                editButton.IsEnabled = true;//Make the edit button available
            }
            else { MessageBox.Show($"No user has been found from the databse"); }

        }

        /// <summary>
        /// Edit user information method after getting the information through find button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //-----------------------------------------------------
            //get information from boxes 
            //-----------------------------------------------------
            int userid = Convert.ToInt32(this.editThroughUserID.Text);
            String username = this.rowEditUserName.Text;
            String password = this.rowEditPassword.Text;
            String type = this.rowEditType.Text;
            Boolean isManager = Convert.ToBoolean(this.rowEditIfManager.Text);
            String fullname = this.rowEditFullName.Text;

            //-----------------------------------------------------
            //delete the original data in the database
            //-----------------------------------------------------
            UserAdmin deleteUser = new UserAdmin(userid);
            deleteUser.AdminDeleteUser();

            //-----------------------------------------------------
            //Add new data into the database
            //-----------------------------------------------------
            UserAdmin useradmin = new UserAdmin(userid, username, password, type, isManager, fullname);

            int flag = useradmin.AdminAddUser();
            if (flag > 0)
            {
                MessageBox.Show($"User (UserID:{userid}) has been edited!");
            }
            else { MessageBox.Show($"No user has been edited."); }

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
