using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingDits
{
    class Program
    {
        static void Main(string[] args)
        {
            int b = 245; //число
            int count = 0; //количество бит равных 1
            for (int i=0; i<8; i++)
            {
                if ((b >> i)%2 == 1)
                    count++;
            }
            Console.WriteLine(count);
            
        }
    }
}
