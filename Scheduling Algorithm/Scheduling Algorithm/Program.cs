using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_Algorithm
{
    class Program
    {
        public static int QUANTUMTIME = 4;
        public static int NULLARRIVALTIME = 99999;

        public class Process
        {
            public string _processName { get; set; }
            public int _burstTime { get; set; }
            public int _arrivalTime { get; set; }
            public int _rrTime { get; set; }
            public Process(string name, int time)
            {
                _processName = name;
                _burstTime = time;
                _arrivalTime = NULLARRIVALTIME;
                _rrTime = 0;
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
            /*foreach(Process item in process)
            {
                item._arrivalTime = item._burstTime;
            }*/
            print(ref process);
        }

        static void SJF(ref List<Process> process)
        {
            //int time = 0;
            List<Process> sjfProcess = process.OrderBy(o => o._burstTime).ToList();
            /*for (int i = 0; i < sjfProcess.Count; i++)
            {
                sjfProcess[i]._arrivalTime = time;
                time+=sjfProcess[i]._burstTime;
            }*/
            print(ref sjfProcess);
        }

        static void RR(ref List<Process> process)
        {
            int tmp = 0, time = 0;
            float sum = 0;
            do
            {
                for (int i = 0; i < process.Count; i++)
                {
                    if( process[i]._burstTime > 0)
                    {
                        if(process[i]._burstTime <= QUANTUMTIME)
                        {
                            tmp = process[i]._burstTime;
                            process[i]._burstTime = 0;
                            process[i]._arrivalTime = time;
                        }
                        else
                        {
                            process[i]._rrTime += QUANTUMTIME;
                            tmp = QUANTUMTIME;
                            process[i]._burstTime -= QUANTUMTIME;
                        }
                        Console.WriteLine("Time: {0}, Process name: {1}, Burst time: {2}.", time, process[i]._processName, tmp);
                        time += tmp;
                    }    
                }
            }
            while (checkRR(ref process));

            for (int i = 0; i < process.Count; i++)
            {
                Console.WriteLine("Process: {0}, WT: {1}",process[i]._processName, process[i]._arrivalTime - process[i]._rrTime);
                sum += (process[i]._arrivalTime - process[i]._rrTime);
            }
            Console.WriteLine("Average arrival time: {0}.", sum / process.Count);
            //Console.WriteLine("Finish all process at {0}", tmp);
        }

        static bool checkRR(ref List<Process> process)
        {
            for( int i = 0; i < process.Count; i++ )
            {
                if (process[i]._burstTime > 0)
                    return true;
            }
            return false;
        }

        static void print(ref List<Process> process)
        {
            int time = 0;
            float sum = 0;
            for ( int i = 0; i < process.Count; i++)
            {
                Console.WriteLine("Process: {0}, Arrival Time: {1}, Burst time: {2}.", process[i]._processName, time, process[i]._burstTime);
                process[i]._arrivalTime = time;
                sum += process[i]._arrivalTime;
                time += process[i]._burstTime;
            }
            Console.WriteLine("Average arrival time: {0}.", sum/process.Count);
        }
    }
}
