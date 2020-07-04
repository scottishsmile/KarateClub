using System;
using System.Collections.Generic;
using System.Text;

namespace KarateClub
{
    // Class for use with the dictionary that saves the database info
    // Needs to be accessed by other classes, hence why not nested inside Database class.
    public class Student
    {
        public bool select { get; set; }
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string doornum { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int postcode { get; set; }
        public string belt { get; set; }
        public string accounttype { get; set; }
        public string joindate { get; set; }
        public string active { get; set; }
    }
}
