using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpeedLimitChecker
{
    /// <summary>
    /// Class that handles csv data and processes it
    /// </summary>
    internal class DataProcesser
    {


        string file_path = "data.csv";
        string file_end = "licence_plates.txt";
        int counter = 0;
        private object fileLock = new object();
        /// <summary>
        /// Method that searches and compares csv data and writes into a file
        /// </summary>
        /// <param name="start"> Start of the lines of csv </param>
        /// <param name="end"> End of the lines of csv </param>
        public void speedLimiter(int start, int end) 
        {
            if (File.Exists(file_path)) { 
                var lines = File.ReadAllLines(file_path);

                if (start < 0) start = 0;
                if (end > lines.Length) end = lines.Length -1;

                for (int i = start; i <= end; i++)
                {
                    string line = lines[i];
                    var cols = line.Split(',');

                    if (cols.Length < 3)
                        continue;

                    if (!int.TryParse(cols[0], out int limit)) continue;
                    if (!int.TryParse(cols[1], out int speed)) continue;

                    if (speed > limit)
                    {
                        Interlocked.Increment(ref counter);

                        string thirdColumn = cols[2];

                        lock (fileLock)
                        {
                            File.AppendAllText(file_end, thirdColumn + Environment.NewLine);
                        }
                    }
                }
                getCounter();
            }
        }

        /// <summary>
        /// Gets count of rule breaks
        /// </summary>
        /// <returns>number of speed limits passed</returns>
        public int getCounter() 
        {
            return counter;
        }

    }
}
