using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;                    // Needed for Log4Net FileInfo
using System.Windows.Documents;
using System.Diagnostics;

namespace KarateClub
{
    // Accepts strings passed in and saves them to a log file.
    // Needs to know the string message you want to save and the type of log - Info Log, Error Log etc.
    // logger( <log string message> , <type of log i.e. Info or Error)

    
    // Uses Log4Net

    public class Logger
    {
        // Log4Net
        // Tell Log4Net what we are wanting to log.
        // System.Reflection... gets the current method and passes it to Log4Net. A little slower but reuseable, it could just be the class name   typeof(Home)  but it works.
        private static readonly log4net.ILog logging = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // Create Log File
        // Need to create in user's temp folder, no idea what the user's file path will be..
        public void createLogFile()
        {
            // Get the file path
            string logDirectory = @"C:\KarateClubLogs";
            string logFilePath = @"C:\KarateClubLogs\Log.txt";

            // Create Log File if it doesn't exist
            if (!(File.Exists(logFilePath)))
            {
                // Create Directory C:\KarateClubLogs
                System.IO.Directory.CreateDirectory(logDirectory);

                using (FileStream fs = System.IO.File.Create(logFilePath))
                {
                    // Add a title to the file   
                    Byte[] title = new UTF8Encoding(true).GetBytes("----Karate Club Logs---- \n");
                    fs.Write(title, 0, title.Length);
                }
            }

            // Tell Log4Net the file path
            log4net.GlobalContext.Properties["LogFileName"] = logFilePath;

        }


        public void log(string logMsg, string logType)
        {
            // Log4Net needs this on .NET Core
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // Log file max size is 25MB, set in log4net.config

            // Log the various kinds of message levels.
            switch (logType)
            {
                case "Info":
                    logging.Info(logMsg);
                    break;
                case "Error":
                    logging.Error(logMsg);
                    break;
                case "Fatal":
                    logging.Fatal(logMsg);
                    break;
                case "Warn":
                    logging.Warn(logMsg);
                    break;
                case "Debug":
                    logging.Debug(logMsg);
                    break;
                default:
                    logging.Error(String.Format("Logger Error. Incorrect type of log message! - {0} & {1}", logType, logMsg));
                    break;
            }
            
        }
    }
}
