using BookStoreLIB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class RegisterDialog : Window
    {

        internal Boolean isManager = false;

        public RegisterDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Password;
            string repassword = this.repassword.Password;
            string fullname = this.fullname.Text;
            bool isManager = (bool)this.ismanager.IsChecked;

            if (username != "" && password != "" && repassword != "" && fullname != "")
            {

                if (password == repassword)
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
                    var valid = atLeast6Characters.IsMatch(password) && letters.IsMatch(password) && numbers.IsMatch(password) && startWithLetter.IsMatch(password) && special.IsMatch(password);
                    if (valid)
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        this.password.Password = ""; this.repassword.Password = "";
                        MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
                    }
                }
                else
                {
                    this.password.Password = ""; this.repassword.Password = "";
                    MessageBox.Show("Your passwords did not match!");
                }
            }
            else
            {
                this.password.Password = ""; this.repassword.Password = "";
                MessageBox.Show("Please fill in all slots.");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
