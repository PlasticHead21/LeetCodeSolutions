using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                checked
                {
                    Int32 temp = 121;             
                Console.WriteLine(Program.IsPalindrome(temp));
                }
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool IsPalindrome(Int32 digit)
        {
            if (digit < 0 | (digit % 10 == 0 && digit != 0))
                return false;
            if(digit < 10)
            {
                return true;
            }
            int revertedNumber = 0;
            while(revertedNumber < digit)
            {               
                revertedNumber = revertedNumber * 10 + digit % 10;
                digit /= 10;
            }

            return revertedNumber == digit || digit == revertedNumber/10 ;
        }

        public static int Reverse(int x)
        {
            int res = 0;
            string temp = "";

            temp = Convert.ToString(x);
            char[] char_arr = temp.ToCharArray();
            if(char_arr[0] == '-')
            Array.Reverse(char_arr, 1, char_arr.Length-1);
            temp = "";

            for (int i = 0; i < char_arr.Length; i++) 
            temp += Char.ToString(char_arr[i]);

            int.TryParse(temp, out res);
            return checked(res);
        }
    }
}
