using System;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[]{'0'};
            var target =
                "1100110101000100011110011101110101111111000110000110000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                    .ToCharArray();
            var gpu = 
            while (array.Length < target.Length && !array.Take(51).SequenceEqual(target.Take(51)))
            {
                var carry = false;
                var i = array.Length-1;
                do
                {
                    if (i < 0)
                    {
                        var temp = new[] {'0'};
                        array = temp.Concat(array).ToArray();
                        i = 0;
                    }
                    if (array[i] == '0')
                    {
                        array[i] = '1';
                        carry = false;
                    }
                    else if (array[i] == '1')
                    {
                        array[i] = '0';
                        carry = true;
                    }
                    i--;
                } while (carry);
                foreach (var bit in array)
                    Console.Write(bit);
                Console.WriteLine();
            }
        }
    }
}
