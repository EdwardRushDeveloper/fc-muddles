using System;


namespace SimpleLogNBinarySearch
{

    class Program
    {
            //Binary Search
            // 1. The Array must already be sorted
            // 2. We will search recursively
            //Time Complexity is O(log n)

        static void Main(string[] args)
        {
           
            int[] sortedArray = new int[] { 1, 2, 5, 7, 9, 13, 15, 19, 35, 500, 1000 };

            int start = 0;
            int end = sortedArray.Length - 1;
            int searchNumber = 1200;
            Console.WriteLine(binarySearch(sortedArray, searchNumber, start, end));
        }

        /// <summary>
        /// The  Binary Search Recursive Function
        /// </summary>
        /// <param name="array">The array to be searching</param>
        /// <param name="searchNumber">The number to be searching for</param>
        /// <param name="startPoint">The New Starting point of the array if(when) the array is shifted to the right</param>
        /// <param name="endPoint">The New Endpoint of the array if(when) the array is shifted to left</param>
        /// <returns>True when the Number is Found</returns>
        static bool binarySearch(int[] array, int searchNumber, int startPoint, int endPoint)
        {
            //when we get to the point when the Start Index is greater than the Endpoint Index, we need to terminate
            //the recursive call stack and return back.
            if(startPoint > endPoint)
            {
                return false;
            }
    
            //get the middle index for our current cursive call
            int midIndex = int.Parse((Math.Floor((decimal.Parse(startPoint.ToString()) + decimal.Parse(endPoint.ToString()))/2)).ToString());
            
            //if the item is found then just return true
            if(array[midIndex]==searchNumber) return true;

            Console.WriteLine(array[midIndex]);
            //now check the left and righ side of the mid point
            //move to the right if the mid index value is greater than the searchNumber
            if(array[midIndex] > searchNumber) 
            {   
                //shift the end point to the left if the current mid index value is greater than the Search Number
                // this will be the current mid index minus 1. This will be set as the new End point
                return binarySearch(array, searchNumber, startPoint, midIndex-1);
            }

             //default, if the current MidIndex value is less than Search Value, then we 
             //will shift the start index by 1 as the new start index and leave the end point as it is.
             return binarySearch(array, searchNumber, midIndex+1, endPoint);


        }

    }

}