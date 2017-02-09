using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Algorithm
{
    class Program
    {
        public static int QUANTUMTIME = 5;

        public class Process
        {
            public string _processName { get; set; }
            public int _burstTime { get; set; }
            public Process(string name, int time)
            {
                _processName = name;
                _burstTime = time;
            }
        } 

        static void Main(string[] args)
        {
            List<Process> processDetail = new List<Process>();
            int NumberOfProcess = 0;
            Console.Write("Enter number of process: ");
            NumberOfProcess = Convert.ToInt32(Console.ReadLine());

            for(int i = 0; i < NumberOfProcess; i++ )
            {
                Console.Write("Burst time of the P{0}: ",i+1);
                processDetail.Add(new Process("Process" + Convert.ToString(i + 1), Convert.ToInt32(Console.ReadLine())));
            }

            Console.WriteLine("\nFirst-Come, First-Served.");
            FCFS(ref processDetail);

            Console.WriteLine("\nShortest-Job-First.");
            SJF(ref processDetail);

            Console.WriteLine("\nRound Robin.");
            RR(ref processDetail);

            Console.ReadKey();
        }

        static void FCFS(ref List<Process> process)
        {
            print(ref process);
        }

        static void SJF(ref List<Process> process)
        {
            List<Process> sjfProcess = process.OrderBy(o => o._burstTime).ToList();
            print(ref sjfProcess);
        }

        static void RR(ref List<Process> process)
        {
            List<Process> rrProcess = process;
            int tmp = 0, time = 0;
            do
            {
                for (int i = 0; i < process.Count; i++)
                {
                    if( process[i]._burstTime > 0)
                    {
                        if(process[i]._burstTime < QUANTUMTIME)
                        {
                            tmp = process[i]._burstTime;
                            process[i]._burstTime = 0;
                        }
                        else
                        {
                            tmp = QUANTUMTIME;
                            process[i]._burstTime -= QUANTUMTIME;
                        }
                        Console.WriteLine("Time: {0}, Process name: {1}, Burst time: {2}.", time, process[i]._processName, tmp);
                        time += tmp;
                    }    
                }

            }
            while (checkRR(ref rrProcess));
        }

        static bool checkRR(ref List<Process> process)
        {
            for( int i = 0; i < process.Count; i++ )
            {
                if (process[i]._burstTime < 0)
                    return false;
            }
            return true;
        }

        static void print(ref List<Process> process)
        {
            int time = 0; 
            for ( int i = 0; i < process.Count; i++)
            {
                Console.WriteLine("Time: {0}, Process name: {1}, Burst time: {2}.", time, process[i]._processName, process[i]._burstTime);
                time += process[i]._burstTime;
            }
            Console.WriteLine("Finish Job at: {0}.", time);
        }
    }
}
