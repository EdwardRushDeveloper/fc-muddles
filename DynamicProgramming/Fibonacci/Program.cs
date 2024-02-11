using System;

namespace Fibonacci
{
    public class Program
    {

            /*
                    Naive Recursive Algorithmn Example

                    A Dynamic Programming Algorithm that uses memoization

                    We compute the Fibonacci Number we will store it in a Dictionary. The Dictionary will
                    be used to access previous work when additional requests are made.

                    Fibonacci sequence
                    0, 1, 1, 2, 3, 5, 8, 13, 21

                    Fibonacci Recursive Definition 
                    F1 = F2 - 1
                    Fn = (Fn-1) + (Fn-2)
            */

        //memoization of calculations already performed so they do not happen again in the right side of the problem (Fn-2)
        static Dictionary<int, int> dictionary = new Dictionary<int, int>();
        public static void Main(string[] args)
        {
            int n =  Fib(9, "Start");
            System.Console.WriteLine($"Final Number {n}");
        }

        /// <summary>
        /// The function to be called recursively
        /// </summary>
        /// <param name="n">The current number being executed in the recursion operation until 1 is reached and the recursive call is completed.</param>
        /// <param name="t">The tag indicating which tree side is being executed.</param>
        /// <returns>The calculation from the formula</returns>
        public static int Fib(int n, string t)
        {
            //memoization of values already calculated
            //if the number has already been calculated with a recursive call
            //just return the number that has already been calculated for n
            int f = 0;
            if(dictionary.TryGetValue(n, out f))
            {
                System.Console.WriteLine($"Dont Compute {n}: f {t}");
                return f;
            }

            if (n <= 1) {
                return n;
            } else 
            {
                System.Console.WriteLine($"Compute {n}, f {t}");
                f =  Fib((n - 1), "One") + Fib((n - 2), "two");
            }          
            
            //memoization of values already calculated so the right side of the tree does not execute
            dictionary.TryAdd(n, f);

            return f;
        }
    }
}