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
            int frame = 3;
            List<int> input = new List<int> { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 0, 3, 2, 1, 2, 0, 1, 7, 0, 1 };
            LRU(input, frame);
            Console.ReadKey();
        }

        static void FIFO(List<int> input, int frame)
        {
            int[] page_frame = new int[frame];
            //Prepare page frame 
            for (int index = 0; input[index] != '\0'; index++)
            {
                input[index] = -1;
            }
        }

        static void Optimal(List<int> input, int frame)
        {
            int[] page_frame = new int[frame];
            int[] next_page_using = new int[frame];
            //Prepare page frame 
            for (int index = 0; input[index] != '\0'; index++)
            {
                input[index] = -1;
            }

            for(int current_input = 0; current_input < input.Count; current_input++)
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
                    //scan the input to select victim page frame
                    for(int scan_input = current_input + 1; scan_input < input.Count; scan_input++)
                    {
                        for (int index = 0; index < page_frame.Count(); index++)
                        {
                            if()
                        }
                    }

                    Display_page_frame(page_frame);
                }
                else
                {
                    Console.WriteLine("--Hit--");
                    hit = false;
                }
            }
        }

        static void LRU(List<int> input, int frame)
        {
            int[] page_used_counter = new int[frame]; //count each not used page frame time.
            int[] page_frame = new int[frame];
            //Prepare page frame 
            for (int index = 0; index < page_frame.Count() ; index++)
            {
                page_frame[index] = -1;
            }

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
                    Console.WriteLine("---Hit---");
                    hit = false;
                }
            }
        }

        static void Display_page_frame(int[] page_frame)
        {
            Console.Write("Page frame:");
            for (int page = 0; page < page_frame.Count(); page++)
            {
                if (page_frame[page] == -1)
                    Console.Write(" -");
                else
                    Console.Write(" {0}", page_frame[page]);
            }
            Console.WriteLine(".");
        }
    }
}
