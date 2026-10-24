namespace Fibonacci
{
    public class Program
    {

            /*
                    Fibonacci with Dynamic Programming

                    Top-Down (Memoization): the recursive definition is used, but each Fibonacci number
                    is stored in a Dictionary the first time it is computed. When the same number is
                    needed again (the right side of the tree, Fn-2) it is read from the Dictionary
                    instead of being computed again.

                    Bottom-Up (Tabulation): start from the base cases and build up to n with a loop.

                    Fibonacci sequence
                    0, 1, 1, 2, 3, 5, 8, 13, 21

                    Fibonacci Recursive Definition
                    F0 = 0
                    F1 = 1
                    Fn = (Fn-1) + (Fn-2)

                    long holds values up to Fib(92). Larger values throw an OverflowException
                    instead of silently wrapping around to a wrong number.
            */

        //number of times Fib is invoked during a run, used to compare memoized and naive recursion
        static int callCount;

        public static void Main(string[] args)
        {
            const int n = 12;

            //Top-Down with memoization, logged so the recursive tree can be followed
            Console.WriteLine($"Top-Down Memoized Fib({n})");
            Console.WriteLine();
            var memo = new Dictionary<int, long>();
            callCount = 0;
            long memoResult = Fib(n, memo);
            int memoCalls = callCount;

            Console.WriteLine();
            Console.WriteLine($"Final Number {memoResult}");
            foreach (var kvp in memo)
            {
                Console.WriteLine($"Fib({kvp.Key}) = {kvp.Value}");
            }

            //Naive recursion (no memo) for comparison. Not logged since it makes hundreds of calls.
            callCount = 0;
            long naiveResult = Fib(n, memo: null, log: false);
            int naiveCalls = callCount;

            //Bottom-Up tabulation, no recursion
            long bottomUpResult = FibBottomUp(n);

            Console.WriteLine();
            Console.WriteLine($"Memoized  Fib({n}) = {memoResult}, {memoCalls} calls");
            Console.WriteLine($"Naive     Fib({n}) = {naiveResult}, {naiveCalls} calls");
            Console.WriteLine($"Bottom-Up Fib({n}) = {bottomUpResult}, no recursion");
        }

        /// <summary>
        /// Top-Down recursive Fibonacci. Pass a dictionary to memoize, or null for naive recursion.
        /// </summary>
        /// <param name="n">The Fibonacci number to compute. Must not be negative.</param>
        /// <param name="memo">Previously computed values, or null to disable memoization.</param>
        /// <param name="side">Which side of the recursive tree this call is on (Start, Left or Right).</param>
        /// <param name="depth">Depth in the recursive tree, used to indent the log so it reads like the tree.</param>
        /// <param name="log">Whether to write each call to the console.</param>
        /// <returns>The nth Fibonacci number</returns>
        public static long Fib(int n, Dictionary<int, long>? memo, string side = "Start", int depth = 0, bool log = true)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(n);
            callCount++;

            string indent = new(' ', depth * 4);

            //memoization of values already calculated
            //if the number has already been calculated with a recursive call
            //just return the number that has already been calculated for n
            if (memo != null && memo.TryGetValue(n, out long cached))
            {
                if (log) Console.WriteLine($"{indent}Fib({n}) [{side}] already computed = {cached}");
                return cached;
            }

            //base case for the recursion to prevent infinite calls
            if (n <= 1)
            {
                if (log) Console.WriteLine($"{indent}Fib({n}) [{side}] base case = {n}");
                return n;
            }

            if (log) Console.WriteLine($"{indent}Fib({n}) [{side}] winding in");

            //First sub problem, calculate all of the left side of the tree first this will be Fn-1
            //Then calculate the right side of the tree Fn-2. If the number has already been calculated
            //it will be returned from the dictionary and not calculated again.
            long leftReturn  = Fib(n - 1, memo, "Left",  depth + 1, log);
            long rightReturn = Fib(n - 2, memo, "Right", depth + 1, log);

            //checked throws an OverflowException instead of wrapping to a negative number past Fib(92)
            long f = checked(leftReturn + rightReturn);
            if (log) Console.WriteLine($"{indent}Fib({n}) winding out = Fib({n - 1}) + Fib({n - 2}) = {leftReturn} + {rightReturn} = {f}");

            //memoization of values already calculated so the right side of the tree does not execute
            memo?.TryAdd(n, f);

            return f;
        }

        /// <summary>
        /// Bottom-Up Fibonacci. Starts from the base cases and keeps only the last two values,
        /// so it runs in O(n) time with O(1) memory and no recursion.
        /// </summary>
        /// <param name="n">The Fibonacci number to compute. Must not be negative.</param>
        /// <returns>The nth Fibonacci number</returns>
        public static long FibBottomUp(int n)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(n);
            if (n <= 1) return n;

            long previous = 0; //Fib(i-2)
            long current  = 1; //Fib(i-1)

            for (int i = 2; i <= n; i++)
            {
                long next = checked(previous + current);
                previous  = current;
                current   = next;
            }

            return current;
        }
    }
}
