using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace KarateClub
{
    public partial class ConfirmPage : Page
    {
        // This page just confirms that the user's changes on Update Page and UpdateChangeRecordPage have been successful.

        string labelMsg;

        public ConfirmPage()
        {
            InitializeComponent();
        }

        // Accept message passed in from UpdateChangeRecordPage and display it.
        // passedMsg is "Database Updated"
        public ConfirmPage(string passedMsg) : this()
        {
            labelMsg = passedMsg;
            this.Loaded += new RoutedEventHandler(ConfirmPage_Loaded);

        }

        void ConfirmPage_Loaded(object sender, RoutedEventArgs e)
        {

            // Display the passed message on the label
            userMessage.Content = labelMsg;

        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            // Navigate to Home.xaml page
            Home homePage = new Home();
            this.NavigationService.Navigate(homePage);

        }

    }
}
