using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_replacement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Choose case: ");
            int choose_case = Convert.ToInt32(Console.ReadLine());
            int frame = 3;
            //List<int> input = new List<int> { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 0, 3, 2, 1, 2, 0, 1, 7, 0, 1 };
            List<int> input = Generate_Testcase(20, choose_case);
            //Console.WriteLine("\nFIFO");
            //FIFO(input, frame);
            Console.WriteLine("\nOptimal");
            Optimal(input, frame);
            //Console.WriteLine("\nLRU");
            //LRU(input, frame);
            Console.ReadKey();
        }

        static void FIFO(List<int> input, int frame)
        {
            int next_victimPage_index = 0;
            //Prepare page frame 
            int[] page_frame = Prepare_page_frame(frame);
            
            foreach (int current_input in input)
            {
                bool isHit = false;
                //is hit: scan page frame
                for(int page_index = 0; page_index < page_frame.Count(); page_index++)
                {
                    if(page_frame[page_index] == current_input)
                    {
                        isHit = true;
                        break;
                    }
                }

                if(!isHit)
                {
                    page_frame[next_victimPage_index] = current_input;
                    next_victimPage_index = (next_victimPage_index + 1) % frame;
                    Display_page_frame(page_frame);
                }
                else
                {
                    Console.WriteLine("\t--Hit--");
                    isHit = false;
                }
            }
        }

        static void Optimal(List<int> input, int frame)
        {
            //Prepare page frame 
            int[] page_frame = Prepare_page_frame(frame);

            for (int current_input = 0; current_input < input.Count; current_input++)
            {
                bool hit = false;
                for (int index = 0; index < page_frame.Count(); index++)
                {
                    if (input[current_input] == page_frame[index])
                    {
                        hit = true;
                        break;
                    }
                }
                //replacement and display page frame
                if(!hit)
                {
                    int[] next_page_using = new int[frame];
                    int max_next_using_time = -1;
                    int victim_page = -1;
                    //scan the input to select victim page frame
                    for (int index = 0; index < page_frame.Count(); index++)
                    {
                        for (int scan_input = current_input + 1; scan_input < input.Count; scan_input++)
                        {
                            if (input[scan_input] != page_frame[index])
                                next_page_using[index]++;
                            else
                                break;
                        }
                    }
                    for (int index = 0; index < page_frame.Count(); index++)
                    {
                        if (next_page_using[index] > max_next_using_time)
                        {
                            max_next_using_time = next_page_using[index];
                            victim_page = index;
                        }
                        if (page_frame[index] == -1)
                        {
                            victim_page = index;
                        }
                        
                    }
                    page_frame[victim_page] = input[current_input];

                    Display_page_frame(page_frame);
                }
                else
                {
                    Console.WriteLine("\t--Hit--");
                    hit = false;
                }
            }
        }

        static void LRU(List<int> input, int frame)
        {
            int[] page_used_counter = new int[frame]; //count each not used page frame time.
            //Prepare page frame 
            int[] page_frame = Prepare_page_frame(frame);

            //LRU Process
            for (int index = 0; index < input.Count; index++)
            {
                int max_time_index = -1;
                int max_time = -1;
                bool hit = false;
                for (int page = 0; page < frame; page++)
                {
                    if (page_frame[page] == input[index])
                    {
                        page_used_counter[page] = 0;
                        hit = true;
                    }
                    else
                    {
                        page_used_counter[page]++;
                        if (page_used_counter[page] > max_time)
                        {
                            max_time = page_used_counter[page];
                            max_time_index = page;
                         }
                    }
                }

                //page fault and Display the page frame
                if (!hit)
                {
                    page_frame[max_time_index] = input[index];
                    page_used_counter[max_time_index] = 0;

                    Display_page_frame(page_frame);
                }
                else
                {
                    Console.WriteLine("\t---Hit---");
                    hit = false;
                }
            }
        }

        static void Display_page_frame(int[] page_frame)
        {
            Console.Write("\tPage frame:");
            for (int page = 0; page < page_frame.Count(); page++)
            {
                if (page_frame[page] == -1)
                    Console.Write(" -");
                else
                    Console.Write(" {0}", page_frame[page]);
            }
            Console.WriteLine(".");
        }

        static int[] Prepare_page_frame(int frame)
        {
            int[] page_frame = new int[frame];
            for (int index = 0; index < page_frame.Count(); index++)
            {
                page_frame[index] = -1;
            }
            return page_frame;
        }
        
        //testcase_type: 1) Distict number, 2) Non-distict number and 3) Pattern
        static List<int> Generate_Testcase(int number_testcase, int testcase_type)
        {
            List<int> testcase = new List<int>();
            System.IO.StreamWriter writer = new System.IO.StreamWriter("test case.txt");
            Random rnd = new Random();
            int random_nItem;

            //if type is distict or non-distict, then shuffle the list before add to text file.
            if (testcase_type == 3)
            {
                List<int> tmp_pattern = new List<int>();
                int n_pattern = rnd.Next(2, 7);
                for(int current_pattern = 0; current_pattern < n_pattern; current_pattern++)
                {
                    tmp_pattern.Add(rnd.Next(0, number_testcase));
                }

                for (int i = 0; i < number_testcase; i += tmp_pattern.Count)
                {
                    //Random the number of items in this transaction.
                    foreach(int item in tmp_pattern)
                    {
                        testcase.Add(item);
                    }
                }
            }
            else
            {
                if (testcase_type == 1)
                {
                    int most_member = (int)(number_testcase * 0.6);
                    random_nItem = rnd.Next(0, number_testcase);
                    for (int i = 0; i < most_member; i++)
                    {
                        //Random the number of items in this transaction.
                        testcase.Add(random_nItem);
                    }
                    for (int i = testcase.Count; i < number_testcase; i++)
                    {
                        //Random the number of items in this transaction.
                        random_nItem = rnd.Next(0, number_testcase);
                        testcase.Add(random_nItem);
                    }
                    testcase = testcase.OrderBy(item => rnd.Next()).ToList();
                }
                else
                {
                    for (int i = 0; i < number_testcase;)
                    {
                        //Random the number of items in this transaction.
                        random_nItem = rnd.Next(0, number_testcase);
                        if(!testcase.Contains(random_nItem))
                        {
                            testcase.Add(random_nItem);
                            i++;
                        }
                    }
                    testcase = testcase.OrderBy(item => rnd.Next()).ToList();
                }
            }

            foreach (int item in testcase)
            {
                writer.Write("{0}, ",item);
            }

            writer.Close();
            
            return testcase;
        }
    }
}
