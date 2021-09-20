using System;

namespace CoinChange
{
    class Program
    {

        /*

        322. Coin Change
        Medium
        Dynamic Programming 


        Space O(N)   : due to the amount array that we create to track the coin count for each amount
        Time  O(n*m) : due to the coin loop inside of the amount loop


        Links and Help Resources 

        It is important to note that they are not asking for the exact coin combinations, just the minimul number of coin combinations to make up the value

        https://leetcode.com/problems/coin-change/

        https://www.youtube.com/watch?v=1R0_7HqNaW0
        https://www.youtube.com/watch?v=jaNZ83Q3QGc&t=271s

        https://medium.com/@safar.nama/coin-change-problem-leetcode-518-f7685bae54c1

        You are given an integer array coins representing coins of different denominations and an integer amount representing a total amount of money.
        Return the fewest number of coins that you need to make up that amount. If that amount of money cannot be made up by any combination of the coins, return -1.
        You may assume that you have an infinite number of each kind of coin.


        Example 1:

        Input: coins = [1,2,5], amount = 11
        Output: 3
        Explanation: 11 = 5 + 5 + 1
        Example 2:

        Input: coins = [2], amount = 3
        Output: -1
        Example 3:

        Input: coins = [1], amount = 0
        Output: 0
        Example 4:

        Input: coins = [1], amount = 1
        Output: 1
        Example 5:

        Input: coins = [1], amount = 2
        Output: 2
        

        Constraints:

            1 <= coins.length <= 12
            1 <= coins[i] <= 231 - 1
            0 <= amount <= 104



        */
        static void Main(string[] args)
        {
           
           /*
                Input: coins = [1,2,5], amount = 11
                Output: 3
                Explanation: 11 = 5 + 5 + 1

                Note, the problem does not ask for the exact coins to put togeher, it is asking for the number 
                of coins.
           */
            int[] coins = new int[] { 1, 2, 5 };
            int amount = 12;
            int combinations = CoinChange(coins, amount);

            System.Console.WriteLine("");
            System.Console.WriteLine (combinations);
        }

        //Coin Change Dynamic Programming Problem
        public static int CoinChange(int[] coins, int amount)
        {

            //creating an array for the designated amount
            //the size of this array is directly related to the Amount value + 1
            //since it is 0 based, but there will never be a zero cents.
            //this will give us our O(N) space complexity.
            int[] dp = new int[amount + 1];
           
            //let us fill our array with dummy values way out of range so we know 
            //when we can update the value in the array index.
            //we will do the amount + 2, but it could easily be amount + 1
            System.Array.Fill(dp, amount + 2);


            //starting from the bottom up, we will start at index 0
            //which is synonomous with 0 cents, 1 will be 1 cent, 2 would be 2 cents etc..
            //set dp[0] to zero because zero cents
            dp[0] = 0;
            /*


                    So let us explain the process for finding the minimum number of coins that it will take to 
                    make up 11 cents.
                    Before we begin, we create a Dynamic Programming Array (dp[]) with the size of the array being 
                    dp[amount+1]. What this means is that when we are at db[7], we are on Amount 7.

                    Using Amount 7 (dp[7]) with the Coin 5 as an example. Let us see we have a value of 2 at dp[7]

                    1. With coin 5, is 5 less than amount 7 in our iteration
                    2. Yes it is, so let us apply our formula. We are on dp[7] in our example currentAmountIndex = 7
                       Applying dp[currentAmountIndex - currentCoinValue] = dp[7-5] = dp[2] = 1
                    3. So the value at dp[2] is 1 indicated by [1] below
                    4. Continue with our formula, take the value at dp[2] and add 1 to it.
                    5. The Value we get is 2. 
                        So (dp[currentAmountIndex - currentCoinValue] = dp[7-5] = dp[2] = 1) + 1 = 2
                    6. Now check the current value at dp[7], if the value of 2 is less than the value at dp[7] then
                       we replace the value at dp[7] with our new value of 2. 
                    7. So the magic here is that the coin combinations like (2+5) is not what the question is asking for.
                       The question is, "What is the least number of coins to make the value 7", the answer is 2 with
                       no indicator on what the coin values are, just that we know that the minimum of coins to make the
                       amount is 2. So dp[7] is replaced with the value 2...if the value 2 is less than the value that is 
                       already there. In this case, it is.
                    

         For Coin 5:
         Amount  ->  0  | 1  | 2  | 3  | 4  | 5  | 6  | 7  |   <- Amount we are looking, but using coin value of 5 
                     ---------------------------------------       
         dp array -> 0  | 1  |[1] | 2  | 2  | 1  | 2  | 2  |


            */

            //now for our outter loop, we need to iterate through each amount
            //element to build combinations                                                                                                                        
            for (int i = 0; i <= amount; i++)
            {
                //in the inner loop we want to iterate through our coins
                for (int c = 0; c < coins.Length; c++)
                {
                    //if the current coin value is less than the current Cents index
                    //we want to take this coin.
                    if (coins[c] <= i)
                    {
                        //set the current cent slot of the dp array
                        //set the current dp value equal to the min amount comparison between the current
                        //dp index, and the value found at dp[i-coins[c]]
                        dp[i] = System.Math.Min(dp[i], 1 + dp[i - coins[c]]);
                        System.Console.WriteLine("");
                        printAmount(dp, coins[c]);
                    }
                }

            }

            return dp[amount] > amount ? -1 : dp[amount];
        }

        /////////////////
        //Function to print the amounts out into a grid layout
        /////////////////
        public static void printAmount(int[] dp, int currentCoin)
        {
            System.Console.Write($"{currentCoin}");

            for(int i = 0; i < dp.Length; i++)
            {
                System.Console.Write($" | {dp[i]} ");
            }
        }
    }
}

