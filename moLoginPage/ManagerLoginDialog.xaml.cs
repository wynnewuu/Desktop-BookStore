using System.Windows;
using System.Text.RegularExpressions;
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class ManagerLoginDialog : Window
    {
       
        public ManagerLoginDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string username = this.manager_nameTextBox.Text;
            string password = this.manager_passwordTextBox.Password;
            if (username != "" && password != "")
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
                MessageBox.Show("Please fill in all slots.");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
