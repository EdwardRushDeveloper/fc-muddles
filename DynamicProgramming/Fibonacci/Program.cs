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
            int n =  Fib(12, "Start");
            System.Console.WriteLine($"Final Number {n}");

            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"Fib({kvp.Key}) = {kvp.Value}");
            }
        }

        /// <summary>
        /// The function to be called recursively
        /// </summary>
        /// <param name="n">The current number being executed in the recursion operation until 1 is reached and the recursive call is completed.</param>
        /// <param name="t">The tag indicating which tree side is being executed.</param>
        /// <returns>The calculation from the formula</returns>
        public static int Fib(int n, string t)
        {
            
            string invocationId = Guid.NewGuid().ToString("N").ToUpper();
            
            //memoization of values already calculated
            //if the number has already been calculated with a recursive call
            //just return the number that has already been calculated for n
            int f = 0;

            if(dictionary.TryGetValue(n, out f))
            {
                System.Console.WriteLine($"Already Computed for Number {n}: Value is {f}: InvoicationId {invocationId}");
                System.Console.WriteLine($"");
                return f;
            }

            //base case for the recursion to prevent infinite calls
            if (n <= 1) 
            {
                Console.WriteLine($"InvocationId {invocationId}: Base Case Reached Fib({n}) = {n}");
                System.Console.WriteLine($"");
                return n;
            } 
            else 
            {
  
                //First sub problem, calculate all of the left side of the tree first this will be Fn-1
                //Then calculate the right side of the tree Fn-2. If the number has already been calculated
                //it will be returned from the dictionary and not calculated again.
                int ls = n - 1;
                int rs = n - 2;
                 
                System.Console.WriteLine($"");
                int leftReturn = Fib((ls), "Left  Side Recursive Call");

                Console.WriteLine($"**********************InvocationId {invocationId}: Winding out to call Right Side Fib({rs}) after Left Side Fib({ls}) = Left Result is {leftReturn} ************************");
                
                System.Console.WriteLine($"");
                int rightReturn = Fib((rs), "Right Side Recursive Call");

                f = leftReturn + rightReturn;
                Console.WriteLine($"InvocationId {invocationId}: Winding Out Results for Number({n}) = Fib({ls}) + Fib({rs}) OR {leftReturn} + {rightReturn} = {f} ");

                System.Console.WriteLine($"");
                System.Console.WriteLine($"");
            }          
            
            //memoization of values already calculated so the right side of the tree does not execute
            Console.WriteLine($"Storing Computed Value for Number({n}) = {f} in Dictionary");
            System.Console.WriteLine($"");
            dictionary.TryAdd(n, f);

            return f;
        }
    }
}