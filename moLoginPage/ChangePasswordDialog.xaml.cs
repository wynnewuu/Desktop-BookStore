using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = this.passwordTextBox.Password;
            string confirmPassword = this.cpasswordTextBox.Password;
            if (newPassword != "" && confirmPassword != "" && newPassword == confirmPassword)
            {
                // at least 6 characters
                var atLeast6Characters = new Regex(@".{6,}");
                // contains letters
                var letters = new Regex(@"\w");
                // contain numbers
                var numbers = new Regex(@"\d");
                // must start with a letter
                var startWithLetter = new Regex(@"^[a-zA-Z].*");
                // no special characters
                var special = new Regex(@"[a-zA-Z0-9]+$");
                // check if entered password is valid
                var valid = atLeast6Characters.IsMatch(newPassword) && letters.IsMatch(newPassword) && numbers.IsMatch(newPassword) && startWithLetter.IsMatch(newPassword) && special.IsMatch(newPassword);
                if (valid)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                    MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
                }
            }
            else
            {
                this.DialogResult = false;
                MessageBox.Show("Passwords do not match.");
            }

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
