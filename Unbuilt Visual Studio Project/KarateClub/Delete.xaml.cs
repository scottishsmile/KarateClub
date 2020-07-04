using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;               // Logs Hyperlink
using System.Windows.Navigation;        // Logs Hyperlink

namespace KarateClub
{
    public partial class Delete : Page
    {

        // Create Class variables.
        int id;
        string firstname;
        string surname;
        List<Student> searchResults = new List<Student>();

        public Delete()
        {
            InitializeComponent();
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            // Navigate to Home.xaml page
            Home homePage = new Home();
            this.NavigationService.Navigate(homePage);

        }

        private void Button_AddMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Add.xaml page
            Add addPage = new Add();
            this.NavigationService.Navigate(addPage);

        }

        private void Button_UpdateMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Update.xaml page
            Update updatePage = new Update();
            this.NavigationService.Navigate(updatePage);
        }

        private void Button_Graphs(object sender, RoutedEventArgs e)
        {
            // Navigate to Update.xaml page
            Charts graphs = new Charts();
            this.NavigationService.Navigate(graphs);
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Hyperlink_Logs(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            // Log4Net Test
            Logger logs = new Logger();
            logs.log("Starting Delete Page.", "Info");

            // FIND DATABASE RECORDS


            // Grab user input
            firstname = box_firstname.Text;
            surname = box_surname.Text;


            // Validate user input

            // Triggers to decide if the form is fully validated
            bool firstname_validationPassed = false;
            bool surname_validationPassed = false;

            Validation validate = new Validation();

            // Validate user string inputs.
            // alphabetValidation() used.
            // 0 is a PASS , greater than > 0 is a FAIL.

            int validate_firstname = validate.alphabetValidation(firstname);
            if (validate_firstname > 0)
            {
                // Failed Validation
                firstname_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Use a-z in text fields, max 100 chars. No Swearing. No blank boxes.";
                firstname_validationPassed = false;
            }
            else
            {
                firstname_ErrorLabel.Content = "";
                firstname_validationPassed = true;
            }

            int validate_surname = validate.alphabetValidation(surname);
            if (validate_surname > 0)
            {
                // Failed Validation
                surname_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Use a-z in text fields, max 100 chars. No Swearing. No blank boxes.";
                surname_validationPassed = false;
            }
            else
            {
                surname_ErrorLabel.Content = "";
                surname_validationPassed = true;
            }


            // FORMATTING
            // Format the user input to be firstletter as capital, the rest as lower case. "John" "Smith" "Blue st"
            Format format = new Format();
            firstname = format.Formatting(firstname);
            surname = format.Formatting(surname);


            // Testing
            logs.log(String.Format("DeletePg Validation firstname: " + firstname), "Info");
            logs.log(String.Format("DeletePg Validation surname: " + surname), "Info");



            // Formatting may return null or WPF may send "" if input box is blank.


            // Firstname and Surname supplied
            if ((firstname != "" && firstname != null ) && (surname != "" && surname != null))
            {
                if (firstname_validationPassed == true && surname_validationPassed == true)
                {
                    // Testing
                    logs.log("First Name and Surname Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();                                                            
                    searchResults = searchDB.DatabaseSearch(DataGridSearch, firstname, surname);     // Returns a List<Student> of the search result.    

                    // Check for no results returned
                    if (searchResults.Count == 0)
                    {
                        Submit_ErrorLabel.Content = "Sorry, No Records Found.";
                    }
                    else
                    {
                        Submit_ErrorLabel.Content = "";     // Clear any error labels
                    }

                }
            }


            // Only Surname supplied
            if ((firstname == "" || firstname == null ) && (surname != "" && surname != null))
            {
                if (surname_validationPassed == true)
                {
                    // Testing
                    logs.log("Surname Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();
                    searchResults = searchDB.DatabaseSurnameSearch(DataGridSearch, surname);          // Returns a List<Student> of the search result. 

                    // Check for no results returned
                    if (searchResults.Count == 0)
                    {
                        Submit_ErrorLabel.Content = "Sorry, No Records Found.";
                    }
                    else
                    {
                        Submit_ErrorLabel.Content = "";     // Clear any error labels
                    }
                }
            }


            // Only Firstname supplied
            if ((firstname != "" && firstname != null) && (surname == "" || surname == null))
            {
                if (firstname_validationPassed == true)
                {
                    logs.log("First Name Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();
                    searchResults = searchDB.DatabaseFirstnameSearch(DataGridSearch, firstname);      // Returns a List<Student> of the search result.

                    // Check for no results returned
                    if (searchResults.Count == 0)
                    {
                        Submit_ErrorLabel.Content = "Sorry, No Records Found.";
                    }
                    else
                    {
                        Submit_ErrorLabel.Content = "";     // Clear any error labels
                    }
                }
            }
        }

            private void Button_Delete(object sender, RoutedEventArgs e)
        {
            // Log4Net Test
            Logger logs = new Logger();
            logs.log("Starting Delete Page.", "Info");

            // DELETE the selected result
            // There may be several records returned by the search (searchDB object)
            // The returned list has a checkbox named "select", use this to identify which results to delete.


            foreach (var result in searchResults)
            {
                if(result.select == true)   // select is true if the checkbox is ticked.
                {
                    // Testing
                    logs.log(String.Format("Deleting Selected Record: {0} {1}", result.firstname.ToString(), result.surname.ToString()), "Info");

                    // Delete Record
                    Database delete = new Database();
                    delete.DatabaseDelete(result.id, result.firstname, result.surname);

                    // Clear Data Grid and provide User Feedback
                    DataGridSearch.ItemsSource = null;
                    DataGridSearch.Items.Refresh();
                    Submit_ErrorLabel.Content = "Record Deleted!";

                }
            }
        }



    }
}
