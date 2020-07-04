using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;       // RegEx

namespace KarateClub
{

    // User Input Validation
    // returning answer = 0 means it passed validation.
    // if answer is not 0 then it failed validation
    class Validation
    {
        // A list of bad words and SQL symbols that could be used in an SQL injection attack.
        List<string> badWords = new List<string>()
        {
            "+",            // SQL symbols
            "=",
            "!",
            "@",
            "'",
            "_",
            "%",
            "[",
            "]",
            "(",
            ")",
            "*",
            "execute",      // SQL commands
            "add",
            "select",
            "delete",
            "update",
            "create",
            "where",
            "from",
            "fuck",         // Swears
            "shit",
            "cunt",
            "bitch",
            "whore",
            "slut",
            "bastard",
            "fake",
            "pretend",

        };
            
        // Validates fields that should only contain alphabet charcaters
        public int alphabetValidation(string userInput)
        {
            // Log4Net
            Logger logs = new Logger();
            logs.log("Starting Validation.", "Info");

            int answer = 0;

            Console.WriteLine(Environment.NewLine);


            // Check length and for blanks
            // Allowing a long query opens up space for SQL injection attacks.
            if (userInput== "" || userInput.Length > 20 || userInput == null)
            {
                logs.log("Length Validation Failed. Input is blank or greater than 100 characters.", "Info");
                answer = answer + 1;    // one added to answer to show fail.
            }
            else
            {
                logs.log(String.Format("Length Validation Passed..." + userInput), "Info");
            }

            // Check for characters only

            // Regex
            // ^ = start at beginning of the string
            // [a-zA-Z] = match any letters lowercase a-z or Uppercase A-Z
            // + = match more than once occurence of this
            // A space is \s. Needed for street names "Main St" and middle names "David John"
            // ? = optional match. the first group of letters need to be matched but the space and the second group are optional
            // $ = end of the string

            if (Regex.IsMatch(userInput, @"^[a-zA-Z]+\s?[a-zA-Z]+$")){

                logs.log(String.Format("Regex Validation Passed...." + userInput), "Info");
            }
            else
            {
                logs.log(String.Format("Regex Validation Failed. Input is not only characters..." + userInput), "Error");
                answer = answer + 1;    // one added to answer to show fail.
            }

            string lowercaseInput = userInput.ToLower();            // Convert string to lowercase


            // Check for bad words & SQL injection

            foreach (string word in badWords) {                     // Cycle through bad world list
                if (lowercaseInput.Contains(word))
                {
                    logs.log(String.Format("Bad Word Validation Failed...." + word + "..." + lowercaseInput), "Error");
                    answer = answer + 1;    // one added to answer to show fail.
                }
                else
                {
                    logs.log(String.Format("Bad Word Validation Passed...." + word + "..." + lowercaseInput), "Info");
                }
            }

            
            return answer;
        }





        // Validates fields that should only contain numbers
        public int numberValidation(string userInput)
        {
            // Log4Net
            Logger logs = new Logger();
            logs.log("Starting Validation.", "Info");

            int answer = 0;     // answer=0 is a Pass, answer > 0 is a Fail.

            // Check length and for blanks

            if (userInput == "" || userInput.Length > 5)
            {
                logs.log(String.Format("Length Validation Failed. Input is blank or greater than 5 digits...." + userInput), "Error");
                answer = answer + 1;    // one added to answer to show fail.
            }
            else
            {
                logs.log(String.Format("Length Validation Passed...." + userInput), "Info");
            }


            // Check for integers only

            // Regex
            // ^ = start atbeginning of the string
            // [0-9] = match any numbers 0-9
            // + = match more than once occurence of this
            // $ = end of the string
            if (Regex.IsMatch(userInput, @"^[0-9]+$"))
            {

                logs.log(String.Format("Regex Validation Passed...." + userInput), "Info");
            }
            else
            {
                logs.log(String.Format("Validation Failed. Input is not only numbers...." + userInput), "Error");
                answer = answer + 1;    // one added to answer to show fail.
            }

            string lowercaseInput = userInput.ToLower();            // Convert string to lowercase

            // Check for bad words & SQL injection

            foreach (string word in badWords)
            {                                                       // Cycle through bad world list
                if (lowercaseInput.Contains(word))
                {
                    logs.log(String.Format("Bad Word Validation Failed...." + word + "..." + lowercaseInput), "Error");
                    answer = answer + 1;                               // one added to answer to show fail.
                }
                else
                {
                    logs.log(String.Format("Bad Word Validation Passed...." + word + "..." + lowercaseInput), "Info");
                }
            }


            return answer;
        }





        // Validates fields that should contain numbers and one alphabet charcater. i.e. door number.
        public int alphaNumValidation(string userInput)
        {
            // Log4Net
            Logger logs = new Logger();
            logs.log("Starting alpha num validation.", "Info");

            int answer = 0;     // answer=0 is a Pass, answer > 0 is a Fail.

            // Check length and for blanks

            if (userInput == "" || userInput.Length > 5)
            {
                logs.log(String.Format("Length Validation Failed. Input is blank or greater than 5 digits...." + userInput), "Error");
                answer = answer + 1;    // one added to answer to show fail.
            }
            else
            {
                logs.log(String.Format("Length Validation Passed...." + userInput), "Info");
            }


            // Check for numbers and one alphabet character

            // Regex
            // ^ = start at beginning of the string
            // [0-9] = match any numbers 0-9
            // + = match more than once occurence of this
            // [a-zA-Z]?  = match one letter a to z "?" means the match is optional. No "+" sign so only one letter.
            // $ = end of the string
            if (Regex.IsMatch(userInput, @"^[0-9]+[a-zA-Z]?$"))
            {

                logs.log(String.Format("Regex Validation Passed...." + userInput), "Info");
            }
            else
            {
                logs.log(String.Format("Validation Failed. Input is not numbers plus one letter...." + userInput), "Error");
                answer = answer + 1;    // one added to answer to show fail.
            }


            string lowercaseInput = userInput.ToLower();            // Convert string to lowercase


            // Check for bad words & SQL injection

            foreach (string word in badWords)
            {                                                       // Cycle through bad world list
                if (lowercaseInput.Contains(word))
                {
                    logs.log(String.Format("Bad Word Validation Failed...." + word + "..." + lowercaseInput), "Error");
                    answer = answer + 1;                               // one added to answer to show fail.
                }
                else
                {
                    logs.log(String.Format("Bad Word Validation Passed...." + word + "..." + lowercaseInput), "Info");
                }
            }


            return answer;
        }


        // Date Picker Validation
        // The date picker does some user validation on it's own, it checks the date is in the right format and makes sense once entered.
        // It does not supply a default date though if one has never been etered.
        // This validation checks for a blank field.
        public int dateValidation(string userInput)
        {
            // Log4Net
            Logger logs = new Logger();
            logs.log("Starting date validation.", "Info");

            int answer = 0;

            if (userInput == "")
            {
                answer = answer + 1;
                logs.log("Date Validation Failed. Input is blank", "Error");
            }
            else
            {
                logs.log("Date Validation Passed", "Info");
            }

            string lowercaseInput = userInput.ToLower();            // Convert string to lowercase


            // Check for bad words & SQL injection

            foreach (string word in badWords)
            {                                                       // Cycle through bad world list
                if (lowercaseInput.Contains(word))
                {
                    logs.log(String.Format("Bad Word Validation Failed...." + word + "..." + lowercaseInput), "Error");
                    answer = answer + 1;                               // one added to answer to show fail.
                }
                else
                {
                    logs.log(String.Format("Bad Word Validation Passed...." + word + "..." + lowercaseInput), "Info");
                }
            }

            return answer;
        }




        // Update Page Checkbox Validation
        // We pass in the search result list (updateSearchResults) and then count how many rows have their checkbox ticked.
        // result.selected is the checkbox.
        // 0 would be a FAIL - none selected
        // 1 is a PASS - correct, only one result chosen.
        // 2 or more is a FAIL.
        public int selectValidation(List<Student> list)
        {
            int answer = 0;

            foreach(Student result in list)
            {
                if(result.select == true)
                {
                    answer = answer + 1;
                }
            }

            return answer;

        }

    }
}
