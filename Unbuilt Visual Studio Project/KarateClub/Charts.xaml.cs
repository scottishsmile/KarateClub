using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;                       // WPF Charts www.lvcharts.net
using LiveCharts.Wpf;
using System.Diagnostics;               // Logs Hyperlink
using System.Windows.Navigation;        // Logs Hyperlink

namespace KarateClub
{

    public partial class Charts : Page
    {

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        Dictionary<string, int> beltCount = new Dictionary<string,int>();


        public Charts()
        {

            // Log4Net Test
            Logger logs = new Logger();
            logs.log("Starting Charts Page.", "Info");

            InitializeComponent();

            // Member Count
            // Need to count the amount of members by belt color for display in bar graph.
            MemberCount();              // Populates Dictionary beltCount.


            // TESTING
            // Log the member counts of each belt.
            logs.log(Environment.NewLine, "Info");
            logs.log("Belt Count", "Info");
            logs.log(String.Format("White: {0}", beltCount["White"]), "Info");
            logs.log(String.Format("Yellow: {0}", beltCount["Yellow"]), "Info");
            logs.log(String.Format("Orange: {0}", beltCount["Orange"]), "Info");
            logs.log(String.Format("Green: {0}", beltCount["Green"]), "Info");
            logs.log(String.Format("Blue: {0}", beltCount["Blue"]), "Info");
            logs.log(String.Format("Purple: {0}", beltCount["Purple"]), "Info");
            logs.log(String.Format("Brown: {0}", beltCount["Brown"]), "Info");
            logs.log(String.Format("Red: {0}", beltCount["Red"]), "Info");
            logs.log(String.Format("Black: {0}", beltCount["Black"]), "Info");


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Members",
                    Values = new ChartValues<int> { beltCount["White"], beltCount["Yellow"], beltCount["Orange"], beltCount["Green"], beltCount["Blue"], beltCount["Purple"], beltCount["Brown"], beltCount["Red"], beltCount["Black"] }
                }

        };

            Labels = new[] { "White", "Yellow", "Orange", "Green", "Blue", "Purple", "Brown", "Red", "Black",};


            Formatter = value => value.ToString("N");

            DataContext = this;

        }

        private Dictionary<string, int> MemberCount()
        {
            // Log4Net
            Logger logs = new Logger();

            // Fetch current members from database
            Database students = new Database();
            var studentList = students.DatabaseRead();

            // Prime Dictionary to list all belts and start count at 0. Prevents null values.
             beltCount.Add("White", 0);
             beltCount.Add("Yellow", 0);
             beltCount.Add("Orange", 0);
             beltCount.Add("Green", 0);
             beltCount.Add("Blue", 0);
             beltCount.Add("Purple", 0);
             beltCount.Add("Brown", 0);
             beltCount.Add("Red", 0);
             beltCount.Add("Black", 0);


            // Go through list and count each belt color
            foreach (Student student in studentList)
            {

                // Loop through the dictionary belt colors
                foreach (var color in beltCount.ToList()){
                    if (color.Key == student.belt)
                    {
                        beltCount[color.Key] = beltCount[color.Key] + 1;

                        // TESTING
                        string data = String.Format("Incrementing Belt Count of " + color.Key.ToString() + " - " + color.Value.ToString());
                        logs.log(data, "Info");
                    }
                }

            }

            return beltCount;
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

        private void Button_DeleteMembers(object sender, RoutedEventArgs e)
        {
            // Navigate to Delete.xaml page
            Delete delPage = new Delete();
            this.NavigationService.Navigate(delPage);
        }

        private void Hyperlink_Logs(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

    }

}
