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
    /// Interaction logic for AccountSettingsDialog.xaml
    /// </summary>
    public partial class AccountSettingsDialog : Window
    {
        UserData userData;
        public AccountSettingsDialog(UserData mainUserData, DataSet dataSet)
        {
            userData = mainUserData;
            this.DataContext = dataSet.Tables["Books"];
            InitializeComponent();
        }
        private void usernameButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeUsernameDialog usernameDialog = new ChangeUsernameDialog();
            usernameDialog.Owner = this;
            usernameDialog.ShowDialog();
            if (usernameDialog.DialogResult == true)
            {
                if (userData.changeUsername(usernameDialog.usernameTextBox.Text) == true)
                {
                    MessageBox.Show("Username successfully updated. Please logout to see changes.");
                }
                else
                {
                    MessageBox.Show("An error has occured, please try again.");
                    this.usernameButton_Click(sender, e);
                }
            }
        }
        private void passwordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordDialog passwordDialog = new ChangePasswordDialog();
            passwordDialog.Owner = this;
            passwordDialog.ShowDialog();
            if (passwordDialog.DialogResult == true)
            {
                if (userData.changePassword(passwordDialog.passwordTextBox.Password) == true)
                {
                    MessageBox.Show("Password successfully updated.");
                }
                else
                {
                    MessageBox.Show("An error has occured, please try again.");
                    this.passwordButton_Click(sender, e);
                }
            }
        }
        private void fullNameButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeFullNameDialog fullNameDialog = new ChangeFullNameDialog();
            fullNameDialog.Owner = this;
            fullNameDialog.ShowDialog();
            if (fullNameDialog.DialogResult == true)
            {
                if (userData.changeFullName(fullNameDialog.fullNameTextBox.Text) == true)
                {
                    MessageBox.Show("Full Name successfully updated.");
                }
                else
                {
                    MessageBox.Show("An error has occured, please try again.");
                    this.fullNameButton_Click(sender, e);
                }
            }
        }
        private void deleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccountDialog deleteDialog = new DeleteAccountDialog();
            deleteDialog.Owner = this;
            deleteDialog.ShowDialog();
            if (deleteDialog.DialogResult == true)
            {
                if (userData.deleteAccount() == true)
                {
                    MessageBox.Show("Account successfully deleted. Please close the application.");
                }
                else
                {
                    MessageBox.Show("An error has occured, please try again.");
                    this.deleteAccountButton_Click(sender, e);
                }
            }
        }
    }
}
