using System;
using System.IO;
using System.Globalization;

class Program
{
    static void Main()
    {
        // Example usage:
        // a = Monthly && b = Daily

        ManageLogsBy("b"); // or ManageLogsBy("Daily"); or can change a = Monthly and b = Daily
    }

    static void ManageLogsBy(string groupBy)
    {
        string logFolder = @"C:\logs"; // Replace with the actual path to your log folder

        if (groupBy != "a" && groupBy != "b")
        {
            Console.WriteLine("Invalid GroupBy parameter. Please use \"a\" or \"b\".");
            return;
        }

        if (!Directory.Exists(logFolder))
        {
            Console.WriteLine($"Directory not found: {logFolder}");
            return;
        }

        string[] files = Directory.GetFiles(logFolder, "*.log");

        if (files.Length == 0)
        {
            Console.WriteLine($"No log files found in the specified folder: {logFolder}");
            return;
        }

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            DateTime logDate;

            if (DateTime.TryParseExact(fileName, "yyyyMMdd-HHmm", null, DateTimeStyles.None, out logDate))
            {
                string newFileName;

                if (groupBy == "a")
                {
                    newFileName = logDate.ToString("MMM-yyyy");
                }
                else
                {
                    newFileName = logDate.ToString("yyyy-MM-dd");
                }

                string newFilePath = Path.Combine(logFolder, newFileName + ".log");

                File.Move(filePath, newFilePath);
                Console.WriteLine($"File \"{fileName}.log\" moved to \"{newFileName}.log\".");
            }
            else
            {
                Console.WriteLine($"Unable to parse date from file: {fileName}.log");
            }
        }
    }
}
