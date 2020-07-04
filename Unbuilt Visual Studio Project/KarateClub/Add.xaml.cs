using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;               // Logs Hyperlink
using System.Windows.Navigation;        // Logs Hyperlink


namespace KarateClub
{
    public partial class Add : Page
    {

        // Create variables for user input. They will be accessable to all below methods due to their position in the scope.
        // The two integer fields doornum and postcode are grabbed as a string postcode_text then later converter to an int after validation
        string firstname;
        string surname;
        string doornum;
        string streetname;
        string city;
        string state;
        string postcode_text;
        int postcode;
        string belt;
        string accounttype;
        string joindate;
        string memactive;


        public Add()
        {
            Logger logs = new Logger();
            logs.log("Starting Add Page.", "Info");

            InitializeComponent();
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            // Navigate to Home.xaml page
            Home homePage = new Home();
            this.NavigationService.Navigate(homePage);

        }

        private void Button_UpdateMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Update.xaml page
            Update updatePage = new Update();
            this.NavigationService.Navigate(updatePage);
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

        private void Hyperlink_Logs(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Button_CreateNewMember(object sender, RoutedEventArgs e)
        {
            //Log4Net
            Logger logs = new Logger();

            // VALIDATE user input first
            // postcode should be integers only and have a max length of 5 digits.
            // doornum can contain digits and one letter. 123B
            // all other variables should be text only and have a max length fo 100 characters.


            // Grab the user input from the form
            firstname = box_firstname.Text;
            surname = box_surname.Text;
            doornum = box_doornum.Text;
            streetname = box_streetname.Text;
            city = box_city.Text;
            state = combobox_state.Text;
            postcode_text = box_postcode.Text;
            belt = combobox_belt.Text;
            accounttype = combobox_accounttype.Text;
            memactive = combobox_memactive.Text;
            joindate = datepicker_joindate.Text;

            // Log User Input
            logs.log(String.Format("Validation firstname: " + firstname), "Info");
            logs.log(String.Format("Validation surname: " + surname), "Info");
            logs.log(String.Format("Validation doornum: " + doornum), "Info");
            logs.log(String.Format("Validation streetname: " + streetname), "Info");
            logs.log(String.Format("Validation postcode: " + postcode_text), "Info");



            // Triggers to decide if the form is fully validated
            // Drop down boxes - state, memactive, belt all have default values asigned so no need to check.
            bool firstname_validationPassed = false;
            bool surname_validationPassed = false;
            bool doornum_validationPassed = false;
            bool streetname_validationPassed = false;
            bool city_validationPassed = false;
            bool postcode_validationPassed = false;
            bool joindate_validationPassed = false;


            Validation validate = new Validation();


            // Validate user string inputs.
            // alphabetValidation() used.
            // 0 is a PASS , greater than > 0 is a FAIL.

            int validate_firstname = validate.alphabetValidation(firstname);
            if (validate_firstname > 0)
            {
                // Failed Validation
                firstname_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Enter only characters a-z in text fields, max 100 characters.";
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
                Submit_ErrorLabel.Content = "Enter only characters a-z in text fields, max 100 characters.";
                surname_validationPassed = false;
            }
            else
            {
                surname_ErrorLabel.Content = "";
                surname_validationPassed = true;
            }


            int validate_streetname = validate.alphabetValidation(streetname);
            if (validate_streetname > 0)
            {
                // Failed Validation
                streetname_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Enter only characters a-z in text fields, max 100 characters.";
                streetname_validationPassed = false;
            }
            else
            {
                streetname_ErrorLabel.Content = "";
                streetname_validationPassed = true;
            }

            int validate_city = validate.alphabetValidation(city);
            if (validate_city > 0)
            {
                // Failed Validation
                city_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Enter only characters a-z in text fields, max 100 characters.";
                city_validationPassed = false;
            }
            else
            {
                city_ErrorLabel.Content = "";
                city_validationPassed = true;
            }



            // Validate user numeric inputs
            // We grab the data as string "box_variable.Text" this is then saved as vatiable_text
            // If it passes validation it is converted to an int as then it's called just variable.
            // 0 is a PASS , greater than > 0 is a FAIL.

            int validate_postcode = validate.numberValidation(postcode_text);
            if (validate_postcode > 0)
            {
                // Failed Validation
                postcode_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Use only numbers 0-9 with a max of 5 digits.";
                postcode_validationPassed = false;
            }
            else
            {
                // Passed Validation
                postcode_ErrorLabel.Content = "";
                postcode_validationPassed = true;
            }

            if (validate_postcode == 0)
            {
                postcode = int.Parse(postcode_text);            // convert to int only if passed validation
            }


            // Door number can be numbers and letters i.e. 12A. alphaNumValidation() is used.
            // Door number will be left as a string as it could contain letters.
            int validate_doornum = validate.alphaNumValidation(doornum);
            if (validate_doornum > 0)
            {
                // Failed Validation
                doornum_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Door Number can be a max of 4 digits and any one character i.e. 1234A";
                doornum_validationPassed = false;
            }
            else
            {
                // Passed Validation
                doornum_ErrorLabel.Content = "";
                doornum_validationPassed = true;
            }


            //DatePicker Picker Validation
            int validate_date = validate.dateValidation(joindate);
            if (validate_date > 0)
            {
                joindate_ErrorLabel.Content = "!";
                Submit_ErrorLabel.Content = "Please enter a join date. Click the calendar button on the right of the box.";
                joindate_validationPassed = false;
            }
            else
            {
                joindate_validationPassed = true;
            }


            // FORMATTING
            // The user can type in all lowercase, uppercase etc.
            // We will format the text to be firstletter as capital, the rest as lower case. "John" "Smith" "Blue st"

            Format format = new Format();

            firstname = format.Formatting(firstname);
            surname = format.Formatting(surname);
            streetname = format.Formatting(streetname);
            city = format.Formatting(city);


            // TESTING
            // Checking to see the formatting of the variables and whats been captured.
            logs.log(String.Format(Environment.NewLine), "Info");
            logs.log(String.Format("full name: " + firstname), "Info");
            logs.log(String.Format("surname: " + surname), "Info");
            logs.log(String.Format("door number: " + doornum), "Info");
            logs.log(String.Format("streetname: " + streetname), "Info");
            logs.log(String.Format("city: " + city), "Info");
            logs.log(String.Format("postcode: " + postcode), "Info");
            logs.log(String.Format("state: " + state), "Info");
            logs.log(String.Format("belt: " + belt), "Info");
            logs.log(String.Format("account type: " + accounttype), "Info");
            logs.log(String.Format("membership active: " + memactive), "Info");
            logs.log(String.Format("Join date: " + joindate), "Info");
            logs.log(String.Format(Environment.NewLine), "Info");


            // Testing Validation Logic
            logs.log("Add Page Validation", "Info");
            logs.log(String.Format("full name: " + firstname_validationPassed), "Info");
            logs.log(String.Format("surname: " + surname_validationPassed), "Info");
            logs.log(String.Format("door number: " + doornum_validationPassed), "Info");
            logs.log(String.Format("streetname: " + streetname_validationPassed), "Info");
            logs.log(String.Format("city: " + city_validationPassed), "Info");
            logs.log(String.Format("postcode: " + postcode_validationPassed), "Info");
            logs.log(String.Format("Join date: " + joindate_validationPassed), "Info");
            logs.log(String.Format(Environment.NewLine), "Info");



            // CREATE NEW MEMBER
            // If the above validation tests have been passed we can go ahead and enter a new member into the database.

            if (firstname_validationPassed == true && surname_validationPassed == true && doornum_validationPassed == true && streetname_validationPassed == true && city_validationPassed == true && postcode_validationPassed == true && joindate_validationPassed == true){

                // Insert the data into the database
                Database insert = new Database();
                insert.DatabaseCreate(surname, firstname, doornum, streetname, city, state, postcode, belt, accounttype, joindate, memactive);
                logs.log("new entry inserted into database.", "Info");

                // Clear all text fields
                box_firstname.Text = "";
                box_surname.Text = "";
                box_doornum.Text = "";
                box_streetname.Text = "";
                box_city.Text = "";
                combobox_state.Text = "";
                box_postcode.Text = "";

                // Tell user insert was successful
                Submit_ErrorLabel.Content = "New User Created!";
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                logs.log("Failed new entry inserted into database.", "Error");
            }



        }






        // COMBOBOXES

        // ComboBox - Membership Active - Yes or NO 
        // This displays the choices in the drop down combobox.
        private void ComboBox_Add_MemActive(object sender, RoutedEventArgs e)
        {
            // List of combobox options
            List<string> memActive = new List<string>();
            memActive.Add("Yes");
            memActive.Add("No");

            // Get the ComboBox
            var comboBox = sender as ComboBox;

            // Assign ItemsSource to the List
            comboBox.ItemsSource = memActive;

            // Select the first item "Yes"
            comboBox.SelectedIndex = 0;
        }


        // ComboBox - Membership Active - Event Handler
        // If the selection is changed to "no" we need an event handler to notify us of this.
        private void ComboBox_Add_MemActive_eventHandler(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox
            var comboBox = sender as ComboBox;

            // Set value to whatever SelectedItem is. "yes" or "no". We can then use value in logic later on.
            string value = comboBox.SelectedItem as string;

        }


        // ComboBox - State - NSW, VIC, QLD, WA, SA, TAS, NT, ACT
        private void ComboBox_Add_State(object sender, RoutedEventArgs e)
        {
            List<string> state = new List<string>();
            state.Add("NSW");
            state.Add("VIC");
            state.Add("QLD");
            state.Add("WA");
            state.Add("SA");
            state.Add("TAS");
            state.Add("NT");
            state.Add("ACT");

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = state;

            // Select the first item "NSW"
            comboBox.SelectedIndex = 0;

        }


        // ComboBox - State - Event Handler
        private void ComboBox_Add_State_eventHandler(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox
            var comboBox = sender as ComboBox;

            // Set value to whatever SelectedItem is.
            string value = comboBox.SelectedItem as string;

        }



        // ComboBox - Belt - White, Yellow, Orange, Green, Blue, Purple, Brown, Red, Black
        private void ComboBox_Add_Belt(object sender, RoutedEventArgs e)
        {
            List<string> belt = new List<string>();
            belt.Add("White");
            belt.Add("Yellow");
            belt.Add("Orange");
            belt.Add("Green");
            belt.Add("Blue");
            belt.Add("Purple");
            belt.Add("Brown");
            belt.Add("Red");
            belt.Add("Black");

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = belt;

            comboBox.SelectedIndex = 0;

        }


        // ComboBox - Belt - Event Handler
        private void ComboBox_Add_Belt_eventHandler(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox
            var comboBox = sender as ComboBox;

            // Set value to whatever SelectedItem is.
            string value = comboBox.SelectedItem as string;

        }


        // ComboBox - Account Type
        private void ComboBox_Add_Type(object sender, RoutedEventArgs e)
        {
            List<string> type = new List<string>();
            type.Add("Adult");
            type.Add("Junior");


            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = type;

            comboBox.SelectedIndex = 0;

        }


        // ComboBox - Belt - Event Handler
        private void ComboBox_Add_Type_eventHandler(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox
            var comboBox = sender as ComboBox;

            // Set value to whatever SelectedItem is.
            string value = comboBox.SelectedItem as string;

        }


        // DATE PICKER BOX
        // The default value displayed dd/mm/yyy or mm/dd/yyy is dependant on your computer's "culture" settings
        // Go to control panel > Region to change them.



    }
}
