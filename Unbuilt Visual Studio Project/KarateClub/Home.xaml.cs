using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;          // For passing DataGrids to other methods.
using System.Diagnostics;               // Logs Hyperlink
using System.Windows.Navigation;        // Logs Hyperlink



namespace KarateClub
{
    public partial class Home : Page
    {

        public Home()
        {

            // Log4Net

            Logger logs = new Logger();
            logs.createLogFile();                               // Create the log file in Temp folder if it doesn't already exit.      
            logs.log("Starting Program! Home Page.", "Info");


            InitializeComponent();

            Database sqlLiteDB = new Database();


            // Build the database if it doesn't already exist.
            sqlLiteDB.DatabaseBuild();


            // Read from database
            // Produces a list of objects
            var students = sqlLiteDB.DatabaseRead();

            // Log test data
            foreach(Student student in students)
            {
                string data  = String.Format("ID: {0} , Name: {1} {2}  has been read from database", student.id, student.firstname, student.surname);
                logs.log(data, "Info");
            }


            // Fill the DataGrid with the info stored in database
            sqlLiteDB.DatabaseFill(DataGridHome);

        }

        



        // BUTTONS
        private void Button_AddMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Add.xaml page
            Add addPage = new Add();
            this.NavigationService.Navigate(addPage);

        }

        private void Button_DeleteMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Delete.xaml page
            Delete delPage = new Delete();
            this.NavigationService.Navigate(delPage);

        }
        private void Button_UpdateMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Update.xaml page
            Update updatePage = new Update();
            this.NavigationService.Navigate(updatePage);

        }

        private void Button_Graphs(object sender, RoutedEventArgs e)
        {
            // Navigate to Charts.xaml page
            Charts graphs = new Charts();
            this.NavigationService.Navigate(graphs);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Hyperlink_Logs(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri){ UseShellExecute = true });
            e.Handled = true;
        }
    }
}
