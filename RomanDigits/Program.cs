using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanDigits
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };
            string num = "";
            string result = "";
            int tmp = 0;
            num = Console.ReadLine();
            num = num.ToUpper();
            var charArr = num.ToCharArray();
            for(int i = 0; i < charArr.Length; i++)
            {
                if(i < charArr.Length - 1)
                {
                    if(dict[charArr[i]] < dict[charArr[i + 1]])
                    {
                        tmp += dict[charArr[i + 1]] - dict[charArr[i]];
                        i++;
                        continue;
                    }
                }
                tmp += dict[charArr[i]];               
            }
            result = tmp.ToString();

            Console.WriteLine(result);
        }
    }
}
