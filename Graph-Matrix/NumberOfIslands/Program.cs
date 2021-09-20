
using System;

namespace NumberOfIslands
{
    class Program
    {

        /*  
        
        200. Number of Islands
        Difficulty : Medium
        DFS Search 
        
            Links and Help Resources 

            Number of Islands Leet Code
            https://leetcode.com/problems/number-of-islands/


            Helpful Links:
            https://www.youtube.com/watch?v=o8S2bO3pmO4
            https://www.geeksforgeeks.org/find-number-of-islands/
            https://www.educative.io/edpresso/dfs-vs-bfs


            Test Input Generation: 
            Creating the LeetCode Grid Syntax. LeetCode uses the 2 dimensional arrays in the form of [][].
            However, many users create their 2 dimesional arrays using [,]. 
            For local testing that you want to port to LeetCode checks, you need to create the test data with [][]
            https://forum.unity.com/threads/c-problems-arrays.199317/

        */

        /*

        Problem Description:

            Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
            An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. 
            You may assume all four edges of the grid are all surrounded by water.

        */

        /*
                Example 1:

                Input: grid = [
                ["1","1","1","1","0"],
                ["1","1","0","1","0"],
                ["1","1","0","0","0"],
                ["0","0","0","0","0"]
                ]
                Output: 1

                Example 2:

                Input: grid = [
                ["1","1","0","0","0"],
                ["1","1","0","0","0"],
                ["0","0","1","0","0"],
                ["0","0","0","1","1"]
                ]
                Output: 3
 

                Constraints:

                m == grid.length
                n == grid[i].length
                1 <= m, n <= 300
                grid[i][j] is '0' or '1'.
        */




        ///////////////////////
        //Main static function to 
        ///////////////////////
        static void Main(string[] args)
        {
            
            int result = NumberOfIslands();

            System.Console.WriteLine($"Number Of Islands Result {result}");

            
        }

            /*
                This will be a DFS Depth First Search (https://www.educative.io/edpresso/dfs-vs-bfs)

                1. With the Grid/Matrix of char values
                2. Iterate through each Row of the Grid/Matrix
                3. Iterate through each Column of each of the Rows
                4. As we iterate through each Row and Column, when we run into a value of '1' then
                   we will continue forward with our DFS function.

            */
            public static int NumberOfIslands(){

                int returnValue = 0;

                /*

                Grid/Matrix for our test 
                        '1','1','1','1','0'
                        '1','1','0','1','0'
                        '1','1','0','0','0'
                        '0','0','0','0','0'

                Expected Output: 1 Island
                */

   
                //we have a grid of matrix values. this example should result in 1
                /*
                char[][] matrix = new char[][]
                { 
                    new char[]{ '1','1','1','1','0'},
                    new char[]{ '1','1','0','1','0' },
                    new char[]{ '1','1','0','0','0' },
                    new char[]{ '0','0','0','0','0' }
                };
                */


                //This example should result in 3
                char[][] matrix = new char[][]
                { 
                    new char[]{ '1','1','0','0','0'},
                    new char[]{ '1','1','0','0','0' },
                    new char[]{ '0','0','1','0','0' },
                    new char[]{ '0','0','0','1','1' }
                };

                //we want to loop through each of the rows and columns looking for 1s that 
                //indicate an Island.
                for(int row = 0; row < matrix.Length; row++ )
                {
                    for(int column = 0; column < matrix[row].Length; column++)
                    {
                         //as we find a one, we will pass the current row number
                         //and the column number down to the DFS function to analyze 
                         //all of the elments around the 
                         if(matrix[row][column] == '1')
                         {
                                System.Console.WriteLine(matrix[row][column]);
                                returnValue += DFS(matrix, row, column);
                         }
                    }
                }
                return returnValue;
            }
            
            
            public static int DFS(char[][] matrix, int currentRow, int currentColumn)
            {

                //base case check
                /*

                    Required 5 checks
                    1. Check to see if the Current Row is less than 0. We dont want to proceed if we are out of bounds on our rows
                    2. Check to see if the Current Row is Greater than or Equal the Grid Length. Again we don't want to proceed outside the bounds of our array
                    3. Check to see of the Current Column is less than 0. Again we don't want to proceed outside the bounds of the our array.
                    4. Check to see if the Current Column is greater than the number of columns of the current row. Out of boundaries check.
                    5. Finally, we want to check to see if the current cell is 0. 

                    Example of a cell : (matrix[currentRow][currentColumn]) see indictor below.
                        '1','1','1','1','0'
                        '1','1','0','1','0'
                        '1','1',(->['0']),'0','0'
                        '0','0','0','0','0'

                */
                if
                ( 
                    currentRow          < 0 
                    || currentRow       >= matrix.Length
                    || currentColumn    < 0 
                    || currentColumn    >= matrix[currentRow].Length
                    || matrix[currentRow][currentColumn] == '0'
                )
                {
                    //if any one of the conditions are met then return 0;
                    return 0;
                }

                //set the current grid item that we are on 
                //this is the sinking of the island, we mark the current item in the grid to
                //[0] indicating it should no longer be counted.
                matrix[currentRow][currentColumn] = '0';

                //now we will do a DFS recursive call for all of the directions up, down and side to side
                //of each of the Rows, and the Columns looking around each of the cells to see if there is a 
                // 1 or 0 

                ///////////////
                ///Process Rows and the Current Column Section
                ///////////////
                //1. Search Down:  We will look at the row just below the current row, and the current column
                //   if this is out of bounds, then 0 will return. If the value is not 0, then the search will continue
                //   to the next row.
                DFS(matrix, currentRow + 1, currentColumn);

                //2. Search Up:  We will look at the row above, and the current column.
                //   if this is out of bounds, then 0 will be returned. If the value is not 0, then the search will continue
                DFS(matrix , currentRow - 1, currentColumn);

                ///////////////
                ///Process the Current Row and the Columns Section
                ///////////////
                //3. Search Right: We will look at the current row, and the one cell to the right.
                //   if this is out of bounds, then 0 will be returned. If the value is not 0, then the search will continue.
                DFS(matrix, currentRow, currentColumn + 1);

                //4. Search Left: We will look at the current row, and the cell to the left.
                //   if this is out of bounds, then 0 will be returned. if the value is not 0, then the search will continue
                DFS(matrix, currentRow, currentColumn - 1);

                return 1;


            }
    }
}
