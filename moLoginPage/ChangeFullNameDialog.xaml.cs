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

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for ChangeFullNameDialog.xaml
    /// </summary>
    public partial class ChangeFullNameDialog : Window
    {
        public ChangeFullNameDialog()
        {
            InitializeComponent();
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string newFullName = this.fullNameTextBox.Text;
            string confirmFullName = this.cfullNameTextBox.Text;
            if (newFullName != "" && confirmFullName != "" && newFullName == confirmFullName)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
                MessageBox.Show("Names do not match.");
            }

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
