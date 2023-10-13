using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5.Properties;

namespace Lab5
{
    internal class FileReader
    {

        public string[] Read(string filename)
        {

            try
            {
                // checks if file exists
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException($"the file '{filename}' does not exist.");
                }


                // reads all lines from file
                string[] lines = File.ReadAllLines(filename);

                return lines;
            }
            catch (Exception ex)
            {
                // handles error that might occur during the file reading
                Console.WriteLine($"Error reading the file: {ex.Message}");
                return new string[0]; // returns an empty string in case an error occured
            }


        }

     


    }
}
