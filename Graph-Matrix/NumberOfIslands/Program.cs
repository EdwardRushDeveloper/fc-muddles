
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
                                returnValue += DFSWithStack(matrix, row, column);
                                //swap with the line above to use the stack version instead of recursion
                                //returnValue += DFSWithStack(matrix, row, column);
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


            /*
                Same DFS as above, but with an explicit Stack<(int,int)> instead of recursion.

                The recursive version can go as deep as the number of land cells in one island.
                On a 300 x 300 grid of all '1's that is 90,000 nested calls, which can overflow
                the .NET call stack. Here the cells waiting to be visited are kept in a Stack
                on the heap, so the depth of the search no longer depends on the call stack.

                1. Check the starting cell the same way DFS does. Return 0 if it is out of bounds or '0'.
                2. Sink the starting cell and push it onto the stack.
                3. While the stack is not empty, pop a cell and look at its 4 neighbours.
                4. Any neighbour that is in bounds and is '1' is sunk and pushed onto the stack.
                5. When the stack is empty the whole island has been sunk, so return 1.

                A cell is sunk when it is pushed, not when it is popped. This stops the same cell
                from being pushed more than once by two different neighbours.
            */
            public static int DFSWithStack(char[][] matrix, int startRow, int startColumn)
            {
                //base case check, same 5 checks as DFS
                if
                (
                    startRow            < 0
                    || startRow         >= matrix.Length
                    || startColumn      < 0
                    || startColumn      >= matrix[startRow].Length
                    || matrix[startRow][startColumn] == '0'
                )
                {
                    return 0;
                }

                /*
                    The 4 directions as (row change, column change).
                    Rows count up going DOWN the grid, and columns count up going RIGHT.

                                    column - 1    column     column + 1
                                  ┌────────────┬────────────┬────────────┐
                        row - 1   │            │    UP      │            │
                                  │            │  (-1,  0)  │            │
                                  ├────────────┼────────────┼────────────┤
                        row       │   LEFT     │  CURRENT   │   RIGHT    │
                                  │  ( 0, -1)  │ (row,col)  │  ( 0,  1)  │
                                  ├────────────┼────────────┼────────────┤
                        row + 1   │            │   DOWN     │            │
                                  │            │  ( 1,  0)  │            │
                                  └────────────┴────────────┴────────────┘

                    The corner cells are never checked. Diagonal land is not joined to the island.

                    Adding a direction to the current cell gives the neighbour:
                        nextRow    = currentRow    + direction.row
                        nextColumn = currentColumn + direction.column

                    Example: the current cell is (2, 2)
                        ( 1,  0)  DOWN   →  (3, 2)
                        (-1,  0)  UP     →  (1, 2)
                        ( 0,  1)  RIGHT  →  (2, 3)
                        ( 0, -1)  LEFT   →  (2, 1)

                    The order is down, up, right, left, the same as the recursive calls in DFS.
                    The order only changes which cell is visited first. Every order gives the same count.
                */
                (int row, int column)[] directions = new (int, int)[]
                {
                    ( 1,  0),   // DOWN
                    (-1,  0),   // UP
                    ( 0,  1),   // RIGHT
                    ( 0, -1)    // LEFT
                };


                //cells that have been sunk but whose neighbours have not been checked yet
                Stack<(int row, int column)> stack = new Stack<(int row, int column)>();

                //sink the starting cell and add it to the stack
                matrix[startRow][startColumn] = '0';
                stack.Push((startRow, startColumn));

                while (stack.Count > 0)
                {
                    (int currentRow, int currentColumn) = stack.Pop();

                    //look at each of the 4 neighbours of the current cell
                    foreach ((int row, int column) direction in directions)
                    {
                        int nextRow = currentRow + direction.row;
                        int nextColumn = currentColumn + direction.column;

                        //skip the neighbour if it is out of bounds or is water
                        if
                        (
                            nextRow             < 0
                            || nextRow          >= matrix.Length
                            || nextColumn       < 0
                            || nextColumn       >= matrix[nextRow].Length
                            || matrix[nextRow][nextColumn] == '0'
                        )
                        {
                            continue;
                        }

                        //sink the neighbour now so it can't be pushed again, then visit it later
                        matrix[nextRow][nextColumn] = '0';
                        stack.Push((nextRow, nextColumn));
                    }
                }

                //the whole island has been sunk
                return 1;
            }
    }
}
