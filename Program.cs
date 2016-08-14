using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCoreStuff
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gk = new GateKeeper();
            int count = 0;
            gk.Listner();
            while(true)
            {
               
                count++;
                if (count == 60)
                {
                    gk.Cancel();
                }

                Console.WriteLine(gk.currentTime.ToString()+"->"+count.ToString());

                Thread.Sleep(1000);
                Console.Clear();

                if(count > 65)
                {
                    break;
                }
            }
           
        }
    }
}
