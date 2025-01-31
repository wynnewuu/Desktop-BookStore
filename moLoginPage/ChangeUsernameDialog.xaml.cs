﻿using System;
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
    /// Interaction logic for ChangeUsernameDialog.xaml
    /// </summary>
    public partial class ChangeUsernameDialog : Window
    {
        public ChangeUsernameDialog()
        {
            InitializeComponent();
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = this.usernameTextBox.Text;
            string confirmUsername = this.cusernameTextBox.Text;
            if (newUsername != "" && confirmUsername != "" && newUsername == confirmUsername)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
                MessageBox.Show("Usernames do not match.");
            }

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
