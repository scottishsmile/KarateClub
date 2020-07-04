using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;          // For passing DataGrids to other methods.

namespace KarateClub
{
    public class Database
    {

        // Database Rules
        // Use parameterised queries @params to help stop SQL injection attacks

        string database_file_path = @"Data Source=.\MemberDB.db";

        public void DatabaseBuild()
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {
                // Create the SQLite Database Table. CodeFirst approach.
                string createTable = @"CREATE TABLE IF NOT EXISTS [Members] (
                        [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                    [Surname] TEXT NOT NULL,
	                    [FirstName] TEXT NOT NULL,
	                    [DoorNum] TEXT NOT NULL,
	                    [StreetName] TEXT NOT NULL,
	                    [City] TEXT NOT NULL,
	                    [State] TEXT NOT NULL,
	                    [PostCode] INTEGER NOT NULL,
	                    [Belt] TEXT NOT NULL,
	                    [AccountType] TEXT NOT NULL,
	                    [JoinDate] TEXT NOT NULL,
	                    [Active] TEXT NOT NULL
                         )";

                // Create the database file.
                // Check if database file exsts first.

                if (File.Exists(database_file_path))
                {
                    System.Data.SQLite.SQLiteConnection.CreateFile("MemberDB.db");                  // Create the database file
                    logs.log("Create Database - MemberDB.db file created", "Info");
                }
                else
                {
                    logs.log(Environment.NewLine, "Info");
                    logs.log("Not Creating Database - MemberDB.db already exists", "Info");
                }


                // Check if the table exists within database file.
                // The SQL statement has a "CREATE TABLE IF NOT EXISTS" statement, so just need to run it.
                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                        // Open the connection
                        cmd.CommandText = createTable;     // Create the table
                        cmd.ExecuteNonQuery();            // Execute the query
                        logs.log("Database Table Created Successfully!", "Info");
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Creation Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }
        }



        // Database CRUD Commands
        // Create Read Update Delete

        //CREATE - Insert new data into database
        public void DatabaseCreate(string surname, string firstname, string doornum, string streetname, string city, string state, int postcode, string belt, string accounttype, string joindate, string memactive)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "INSERT INTO Members(Surname, FirstName, DoorNum, StreetName, City, State, PostCode, Belt, AccountType, JoinDate, Active) VALUES(@surname, @firstname, @doornum, @streetname, @city, @state, @postcode, @belt, @accounttype, @joindate, @memactive)";


                // Check if the table exists within database file.
                // The SQL statement has a "CREATE TABLE IF NOT EXISTS" statement, so just need to run it.
                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@firstname", firstname);
                        cmd.Parameters.AddWithValue("@doornum", doornum);
                        cmd.Parameters.AddWithValue("@streetname", streetname);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@state", state);
                        cmd.Parameters.AddWithValue("@postcode", postcode);
                        cmd.Parameters.AddWithValue("@belt", belt);
                        cmd.Parameters.AddWithValue("@accounttype", accounttype);
                        cmd.Parameters.AddWithValue("@joindate", joindate);
                        cmd.Parameters.AddWithValue("@memactive", memactive);

                        cmd.CommandText = sqlCmd;        // The insert command is a SQL string passed into this function.

                        cmd.ExecuteNonQuery();              // Execute the query

                        logs.log(Environment.NewLine, "Info");
                        logs.log("Database Insert Successfull!", "Info");
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Insert Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }


        }



        // READ
        public List<Student> DatabaseRead()
        {
            // Log4Net
            Logger logs = new Logger();

            List<Student> students = new List<Student>();

            try
            {
                string database_file_path = @"Data Source=.\MemberDB.db";

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection
                        cmd.CommandText = "Select * FROM Members";      // Select all rows

                        using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                        {

                            // Read the database
                            while (reader.Read())
                            {

                                // Add whats read from the database to a list so it can be accessed later
                                students.Add(new Student()
                                {
                                    id = Convert.ToInt32(reader["id"]),
                                    firstname = Convert.ToString(reader["Firstname"]),
                                    surname = Convert.ToString(reader["Surname"]),
                                    doornum = Convert.ToString(reader["DoorNum"]),
                                    streetname = Convert.ToString(reader["StreetName"]),
                                    city = Convert.ToString(reader["City"]),
                                    state = Convert.ToString(reader["State"]),
                                    postcode = Convert.ToInt32(reader["PostCode"]),
                                    belt = Convert.ToString(reader["Belt"]),
                                    accounttype = Convert.ToString(reader["AccountType"]),
                                    joindate = Convert.ToString(reader["JoinDate"]),
                                    active = Convert.ToString(reader["Active"]),
                                });

                            }
                        }
                    }
                }

                logs.log(Environment.NewLine, "Info");
                logs.log("Database Read Successfully!", "Info");

            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Read Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }

            return students;        // Send the updated students list back containing all the database data.

        }



        // UPDATE
        public void DatabaseUpdate(int id, string surname, string firstname, string doornum, string streetname, string city, string state, int postcode, string belt, string accounttype, string joindate, string memactive)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "UPDATE Members SET Surname = @surname, FirstName = @firstname, DoorNum = @doornum, StreetName = @streetname, City = @city, State = @state, PostCode = @postcode, Belt = @belt, AccountType = @accounttype, JoinDate = @joindate, Active = @memactive  WHERE ID = @id";

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@firstname", firstname);
                        cmd.Parameters.AddWithValue("@doornum", doornum);
                        cmd.Parameters.AddWithValue("@streetname", streetname);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@state", state);
                        cmd.Parameters.AddWithValue("@postcode", postcode);
                        cmd.Parameters.AddWithValue("@belt", belt);
                        cmd.Parameters.AddWithValue("@accounttype", accounttype);
                        cmd.Parameters.AddWithValue("@joindate", joindate);
                        cmd.Parameters.AddWithValue("@memactive", memactive);

                        cmd.CommandText = sqlCmd;
                        cmd.ExecuteNonQuery();              // Execute the query
                    }
                }

                logs.log(Environment.NewLine, "Info");
                logs.log("Database Updated Successfully!", "Info");

            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Update Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }


        }



        // DELETE
        // ID should be enough to delete a record but we will use names as well as a double check.
        // To avoid SQL injection use @parameters in SQL query and user input has been validated beforehand. No !@#''_ characters, No select etc statements and length limited.
        public void DatabaseDelete(int id, string firstname, string surname)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "DELETE FROM Members WHERE id = @id AND Surname = @surname AND FirstName = @firstname";

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                     // Open database connection

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@firstname", firstname);

                        cmd.CommandText = sqlCmd;      // Get the passed in command
                        cmd.ExecuteNonQuery();          // Execute the delete


                    }
                }
                logs.log(Environment.NewLine, "Info");
                logs.log("Database Record Deleted!", "Info");
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Delete Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }
        }


        // SEARCH

        // Firstname AND Surname
        public List<Student> DatabaseSearch(DataGrid DataGrid, string firstname, string surname)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "Select * FROM Members WHERE Surname LIKE @surname AND FirstName LIKE @firstname";

                List<Student> searchResult = new List<Student>();

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        surname = surname + "%";            // The % is the wildcard * character for the SQL LIKE statement
                        firstname = firstname + "%";

                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@firstname", firstname);

                        cmd.CommandText = sqlCmd;

                        // Read the selected data into a list
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchResult.Add(new Student()
                                {
                                    select = false,                                           // Checkbox needed to confirm selection
                                    id = Convert.ToInt32(reader["id"]),
                                    firstname = Convert.ToString(reader["Firstname"]),
                                    surname = Convert.ToString(reader["Surname"]),
                                    doornum = Convert.ToString(reader["DoorNum"]),
                                    streetname = Convert.ToString(reader["StreetName"]),
                                    city = Convert.ToString(reader["City"]),
                                    state = Convert.ToString(reader["State"]),
                                    postcode = Convert.ToInt32(reader["PostCode"]),
                                    belt = Convert.ToString(reader["Belt"]),
                                    accounttype = Convert.ToString(reader["AccountType"]),
                                    joindate = Convert.ToString(reader["JoinDate"]),
                                    active = Convert.ToString(reader["Active"]),
                                });
                            }
                        }

                        DataGrid.ItemsSource = searchResult;                                    // Bind list to the Datagrid. Displays the results in the grid.

                        return searchResult;                                                    // Return the search result as a list.
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database First and Surname Search Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");

                return null;
            }
        }

        // Surname ONLY
        public List<Student> DatabaseSurnameSearch(DataGrid DataGrid, string surname)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "Select * FROM Members WHERE Surname LIKE @surname";

                List<Student> searchResult = new List<Student>();

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        surname = surname + "%";            // The % is the wildcard * character for the SQL LIKE statement

                        cmd.Parameters.AddWithValue("@surname", surname);

                        cmd.CommandText = sqlCmd;

                        // Read the selected data into a list
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchResult.Add(new Student()
                                {
                                    select = false,                                           // Checkbox needed to confirm selection
                                    id = Convert.ToInt32(reader["id"]),
                                    firstname = Convert.ToString(reader["Firstname"]),
                                    surname = Convert.ToString(reader["Surname"]),
                                    doornum = Convert.ToString(reader["DoorNum"]),
                                    streetname = Convert.ToString(reader["StreetName"]),
                                    city = Convert.ToString(reader["City"]),
                                    state = Convert.ToString(reader["State"]),
                                    postcode = Convert.ToInt32(reader["PostCode"]),
                                    belt = Convert.ToString(reader["Belt"]),
                                    accounttype = Convert.ToString(reader["AccountType"]),
                                    joindate = Convert.ToString(reader["JoinDate"]),
                                    active = Convert.ToString(reader["Active"]),
                                });
                            }
                        }

                        DataGrid.ItemsSource = searchResult;                                    // Bind list to the Datagrid. Displays the results in the grid.

                        return searchResult;                                                    // Return the search result as a list.
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Surname Search Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");

                return null;
            }
        }


        // Firstname ONLY
        public List<Student> DatabaseFirstnameSearch(DataGrid DataGrid, string firstname)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "Select * FROM Members WHERE FirstName LIKE @firstname";

                List<Student> searchResult = new List<Student>();

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        firstname = firstname + "%";        // SQL LIKE wildcard is % not *

                        cmd.Parameters.AddWithValue("@firstname", firstname);

                        cmd.CommandText = sqlCmd;

                        // Read the selected data into a list
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchResult.Add(new Student()
                                {
                                    select = false,                                           // Checkbox needed to confirm selection
                                    id = Convert.ToInt32(reader["id"]),
                                    firstname = Convert.ToString(reader["Firstname"]),
                                    surname = Convert.ToString(reader["Surname"]),
                                    doornum = Convert.ToString(reader["DoorNum"]),
                                    streetname = Convert.ToString(reader["StreetName"]),
                                    city = Convert.ToString(reader["City"]),
                                    state = Convert.ToString(reader["State"]),
                                    postcode = Convert.ToInt32(reader["PostCode"]),
                                    belt = Convert.ToString(reader["Belt"]),
                                    accounttype = Convert.ToString(reader["AccountType"]),
                                    joindate = Convert.ToString(reader["JoinDate"]),
                                    active = Convert.ToString(reader["Active"]),
                                });
                            }
                        }

                        DataGrid.ItemsSource = searchResult;                                    // Bind list to the Datagrid. Displays the results in the grid.

                        return searchResult;                                                    // Return the search result as a list.
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database FirstName Search Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");

                return null;
            }
        }




        // DATAGRID CONNECTION
        // Fill DataGrid with the Database
        // We are passing the DataGridHome from Home.xaml.cs to here. Need using System.Windows.Controls
        // !!!! Datagrid must have AutoGenerateColumns="True" to see data in columns !!!
        public void DatabaseFill(DataGrid DataGrid)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {
                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();

                        // Select all the data
                        SQLiteCommand sqlcmd;
                        sqlcmd = con.CreateCommand();
                        sqlcmd.CommandText = "SELECT * FROM Members";


                        // Fill the SQL DATA ADAPTER with the database table to display the changes.
                        SQLiteDataAdapter sda = new SQLiteDataAdapter(sqlcmd);
                        DataTable dt = new DataTable("Members");
                        sda.Fill(dt);
                        DataGrid.ItemsSource = dt.DefaultView;
                        sda.Update(dt);                                 // You need to update the datagrid to show the changes.

                    }
                }

                logs.log(Environment.NewLine, "Info");
                logs.log("Database Filled Successfully!", "Info");
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Fill Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
            }
        }



        // To get checkboxes returned when filling the database we need to return a list that includes the select parameter.
        public List<Student> DatabaseFillCheckbox(DataGrid DataGrid)
        {
            // Log4Net
            Logger logs = new Logger();

            try
            {

                string sqlCmd = "Select * FROM Members ";

                List<Student> searchResult = new List<Student>();

                using (var con = new SQLiteConnection(database_file_path))
                {
                    using (var cmd = new SQLiteCommand(con))
                    {
                        con.Open();                         // Open the connection

                        cmd.CommandText = sqlCmd;

                        // Read the selected data into a list
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchResult.Add(new Student()
                                {
                                    select = false,                                           // Checkbox needed to confirm selection
                                    id = Convert.ToInt32(reader["id"]),
                                    firstname = Convert.ToString(reader["Firstname"]),
                                    surname = Convert.ToString(reader["Surname"]),
                                    doornum = Convert.ToString(reader["DoorNum"]),
                                    streetname = Convert.ToString(reader["StreetName"]),
                                    city = Convert.ToString(reader["City"]),
                                    state = Convert.ToString(reader["State"]),
                                    postcode = Convert.ToInt32(reader["PostCode"]),
                                    belt = Convert.ToString(reader["Belt"]),
                                    accounttype = Convert.ToString(reader["AccountType"]),
                                    joindate = Convert.ToString(reader["JoinDate"]),
                                    active = Convert.ToString(reader["Active"]),
                                });
                            }
                        }

                        DataGrid.ItemsSource = searchResult;                                    // Bind list to the Datagrid. Displays the results in the grid.

                        return searchResult;                                                    // Return the search result as a list.
                    }
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Database Fill Checkbox Search Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");

                return null;
            }
        }
    }
}
