using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;               // Logs Hyperlink
using System.Windows.Navigation;        // Logs Hyperlink

namespace KarateClub
{

    public delegate void DataTransferDelegate(int record_id);

    public partial class Update : Page
    {

        // Create Class variables.
        int id;
        string firstname;
        string surname;
        List<Student> updateDefaultList = new List<Student>();
        List<Student> updateSearchResults = new List<Student>();
        List<Student> passedList = new List<Student>();


        public Update()
        {
            InitializeComponent();

            Database updateDB = new Database();
            updateDefaultList = updateDB.DatabaseFillCheckbox(DataGridUpdate);              // Fill Datagrid with a list of all database members including a checkbox.

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

        private void Button_DeleteMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Delete.xaml page
            Delete delPage = new Delete();
            this.NavigationService.Navigate(delPage);
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
            logs.log("Starting Update Page.", "Info");

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
            if ((firstname != "" && firstname != null) && (surname != "" && surname != null))
            {
                if (firstname_validationPassed == true && surname_validationPassed == true)
                {
                    // Logs
                    logs.log("First Name and Surname Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();
                    updateSearchResults = searchDB.DatabaseSearch(DataGridUpdate, firstname, surname);     // Returns a List<Student> of the search result.    

                    // Check for no results returned
                    if (updateSearchResults.Count == 0)
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
            if ((firstname == "" || firstname == null) && (surname != "" && surname != null))
            {
                if (surname_validationPassed == true)
                {
                    // Logs
                    logs.log("Surname Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();
                    updateSearchResults = searchDB.DatabaseSurnameSearch(DataGridUpdate, surname);          // Returns a List<Student> of the search result. 

                    // Check for no results returned
                    if (updateSearchResults.Count == 0)
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
                    //Logs
                    logs.log("First Name Search", "Info");
                    logs.log(Environment.NewLine, "Info");

                    // Search the database
                    // Update the DataGrid with the search result and get a List of the search result plus a checkbox named "select"
                    Database searchDB = new Database();
                    updateSearchResults = searchDB.DatabaseFirstnameSearch(DataGridUpdate, firstname);      // Returns a List<Student> of the search result.

                    // Check for no results returned
                    if (updateSearchResults.Count == 0)
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

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            // Log4Net Test
            Logger logs = new Logger();
            logs.log("Button_Update Pressed", "Info");

            // Checkbox Validation.
            // User can only choose 1 result.
            // valAnswer = 0 would be a FAIL - none selected
            // valAnswer = 1 is a PASS - correct, only one result chosen.
            // valAnswer = 2 or more is a FAIL.

            Validation checkBoxVal = new Validation();

            // Two lists may be presented for validation
            // 1 - User does a search to find record. The list from the search is updateSearchResults
            // 2 - User just clicks a checkbox once the page loads. The default list loaded when the page loads is updateDefaultList


            // 1 - User Searches
            int valSearchAnswer = checkBoxVal.selectValidation(updateSearchResults);

            if (valSearchAnswer == 1)
            {
                foreach (Student result in updateSearchResults)
                {
                    if (result.select == true)
                    {
                        // Copy the record from the search results to a new list passedList, so I can send it to the next page.
                        // An alternative solution would be to send only the record ID and then fetch the details from the database on the next page. But we already have all the data here anyway, soo...
                        passedList.Add(new Student()
                        {
                            id = result.id,
                            firstname = result.firstname,
                            surname = result.surname,
                            doornum = result.doornum,
                            streetname = result.streetname,
                            city = result.city,
                            state = result.state,
                            postcode = result.postcode,
                            belt = result.belt,
                            accounttype = result.accounttype,
                            joindate = result.joindate,
                            active = result.active,
                        });

                        break;          // We process the first checked box only. This stops users selecting multiple boxes.
                    }
                }

                // Navigate to UpdateChangeRecord.xaml page
                UpdateChangeRecord changePage = new UpdateChangeRecord(passedList);           // Pass List of selected 
                this.NavigationService.Navigate(changePage);
            }
            else
            {
                // User Error Reporting
                Submit_ErrorLabel.Content = "Choose ONE checkbox.";

                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Button_Update() Search List Validation FAIL. Checkboxes selected = {0}", valSearchAnswer), "Error");
                logs.log(Environment.NewLine, "Info");
            }



            // 2 - User selects from default List
            int valDefaultListAnswer = checkBoxVal.selectValidation(updateDefaultList);

            if (valDefaultListAnswer == 1)
            {
                foreach (Student result in updateDefaultList)
                {
                    if (result.select == true)
                    {
                        // Copy the record from the search results to a new list passedList, so I can send it to the next page.
                        // An alternative solution would be to send only the record ID and then fetch the details from the database on the next page. But we already have all the data here anyway, soo...
                        passedList.Add(new Student()
                        {
                            id = result.id,
                            firstname = result.firstname,
                            surname = result.surname,
                            doornum = result.doornum,
                            streetname = result.streetname,
                            city = result.city,
                            state = result.state,
                            postcode = result.postcode,
                            belt = result.belt,
                            accounttype = result.accounttype,
                            joindate = result.joindate,
                            active = result.active,
                        });

                        break;          // We process the first checked box only. This stops users selecting multiple boxes.
                    }
                }

                // Navigate to UpdateChangeRecord.xaml page
                UpdateChangeRecord changePage = new UpdateChangeRecord(passedList);           // Pass List of selected 
                this.NavigationService.Navigate(changePage);
            }
            else
            {
                // User Error Reporting
                Submit_ErrorLabel.Content = "Choose ONE checkbox.";

                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Button_Update() Default List Validation FAIL. Checkboxes selected = {0}", valDefaultListAnswer), "Error");
                logs.log(Environment.NewLine, "Info");
            }


        }
    }
}
