using System;
using System.Collections.Generic;
using System.Text;

namespace KarateClub
{
    class Format
    {
        // FORMATTING
        // The user can type in all lowercase, uppercase etc.
        // We will format the text to be firstletter as capital, the rest as lower case. "John" "Smith" "Blue st"

        public string Formatting(string input)
        {
            // Log4Net Test
            Logger logs = new Logger();
            logs.log("Starting Program! Home Page.", "Info");

            try
            {
                if (input != "" && input != null)
                {
                    // Change the whole string to lowercase
                    input = input.ToLower();

                    // Convert the string to an array of letters
                    char[] array = input.ToCharArray();

                    // Change the first letter to uppercase
                    array[0] = char.ToUpper(array[0]);

                    // Convert array back to a single string
                    input = string.Join("", array);

                    return input;
                }
                else
                {
                    logs.log(Environment.NewLine, "Info");
                    logs.log("Format Info - Input was blank or Null", "Error");
                    return input;
                }
            }
            catch (Exception ex)
            {
                logs.log(Environment.NewLine, "Info");
                logs.log(String.Format("Format Error! - " + ex.Message), "Error");
                logs.log(String.Format("More Info - " + ex.ToString()), "Error");
                return null;
            }
        }

    }
}
