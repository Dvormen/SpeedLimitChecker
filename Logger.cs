using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitChecker
{
    internal class Logger
    {
        /// <summary>
        /// This method writes into Log.txt
        /// </summary>
        /// <param name="info"> Number of lines that the program has written </param>
        /// <param name="file"> File name </param>
        public void logWrite(int info, string file) 
        {
            File.AppendAllText("Log.txt","[" + DateTime.Now + "] Written into file " + file + " " + info + " lines" + Environment.NewLine);
        }

    }
}
