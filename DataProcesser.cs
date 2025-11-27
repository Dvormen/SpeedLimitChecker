using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpeedLimitChecker
{
    internal class DataProcesser
    {
        string file_path = "data.csv";
        int counter = 0;
        private object fileLock = new object();

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
                            File.AppendAllText("licence_plates.txt", thirdColumn + Environment.NewLine);
                        }
                    }
                }
                getCounter();
            }
        }


        public int getCounter() 
        {
            return counter;
        }

    }
}
