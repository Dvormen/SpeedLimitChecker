using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitChecker
{
    /// <summary>
    /// Class that creates threads and handles missinputs
    /// </summary>
    internal class Threads
    {
       public Threads() 
       {

            Console.WriteLine("Enter line count:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value))
            {
                int max = int.Parse(input);
                DataProcesser dp = new DataProcesser();
                Thread thread1 = new Thread(() => dp.speedLimiter(0, max/4));
                Thread thread2 = new Thread(() => dp.speedLimiter(max/4, max/4*2));
                Thread thread3 = new Thread(() => dp.speedLimiter(max/4*2, max/4*3));
                Thread thread4 = new Thread(() => dp.speedLimiter(max/4*3, max));

                thread1.Start();
                thread2.Start();
                thread3.Start();
                thread4.Start();

                thread1.Join();
                thread2.Join();
                thread3.Join();
                thread4.Join();

                Console.WriteLine("There were "+dp.getCounter()+" cases of driving over the limit");
                Console.WriteLine("Press Enter to close");
                Console.ReadLine();
            }
            else 
            {
                Console.WriteLine("Incorrect input");
            }
        }
    }
}
