using System;

namespace _1st
{
    class Program
    {
        static int Main(string [] args)
        {
            if(args.Length < 3)
            {
                Console.WriteLine($"Progrram requress 3 args. But there is only {args.Length}");
                return 1;
            }

            var isVal1 = int.TryParse(args[0], out var val1);
            var operation = args[1];
            var isVal2 = int.TryParse(args[2], out var val2);

            if (!isVal1)
            {
                Console.WriteLine($"value 1 is not int");
                return 1;
            }
            if (!isVal2)
            {
                Console.WriteLine($"value 2 is not int");
                return 1;
            }

            var result = operation switch
            {
                "+" => val1 + val2,
                "-" => val1 - val2,
                "*" => val1 * val2,
                "/" => val1 / val2,
                _ => 0,
            };
            Console.WriteLine($"Result : {result}");

            return 0;
        }
    }
}
