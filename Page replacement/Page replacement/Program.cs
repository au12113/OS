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

        }

        static void FIFO(List<int> input, int frame)
        {
            int[] page_frame = new int[frame];
            //Prepare page frame 
            for(int index = 0; input[index] != '\0'; index++)
            {
                input[index] = -1;
            }

            //FIFO Process
            for(int index = 0; input[index] != '\0'; index++)
            {
                //condition

                //scan page frame
                for()
                {

                }
            }
        }

        static void Optimal()
        {

        }

        static void LRU()
        {

        }
    }
}
