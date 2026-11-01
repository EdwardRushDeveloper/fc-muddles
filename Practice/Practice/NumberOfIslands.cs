using System;

namespace Practice;

public class NumberOfIslands
{


    public static void Execute()
    {
        Console.WriteLine("******************** Number of Islands");
        //There are three islands in this matrix
        char[][] matrix = new char[][]
        {
                    new char[]{ '1','1','0','0','0'},
                    new char[]{ '1','1','0','0','0' },
                    new char[]{ '0','0','1','0','0' },
                    new char[]{ '0','0','0','1','1' }
        };

        int result = DFSP2(matrix);

        Console.WriteLine($"Number Of Islands Result {result}");
    }

    public static int DFSP2(char[][] matrix)
    {
        int returnValue = 0;

        for (int row = 0; row < matrix.Length; row++)
        {
            Console.WriteLine($"Row Number {row}");
            for (int column = 0; column < matrix[row].Length; column++)
            {
                //if the current row and column value is 1, then we need to check up,down,right and left and flag those as visited
                if (matrix[row][column] == '1')
                {
                    returnValue += DFSP2Work(matrix, row, column);
                }
            }
        }
        return returnValue;
    }

    public static int DFSP2Work(char[][] matrix, int startRow, int startColumn)
    {
        //we need to do five checks

        if (
            startRow >= matrix.Length
            || startColumn < 0
         )
        {
            return 0;
        }

        //create a tuple array to flag our directions
        (int row, int column)[] directions = new (int, int)[]
        {
                    ( 1,  0),   // DOWN
                    (-1,  0),   // UP
                    ( 0,  1),   // RIGHT
                    ( 0, -1)    // LEFT

        };

        
        //to prevent from running out of space, use a stack
        //the stack is used for the looping mechanism
        Stack<(int row, int column)> stack = new Stack<(int row, int column)>();


        //initialize and sink the first cell using the provided indexes
        matrix[startRow][startColumn] = '0';
        stack.Push((startRow, startColumn));

        //what do we do from here

        while(stack.Count > 0)
        {
            //pop an item off of the stack
            (int currentRow, int currentColumn)= stack.Pop();

            //loop through all of the directions an add the ones that are not visited
            foreach((int row, int column)direction in directions)
            {

                //start looking up, down left and right based on the current direction

                int nextRow = currentRow + direction.row;
                int nextColumn = currentColumn + direction.column;

                if(
                    nextRow < 0 //Check the Top: see if we are trying to go beyond the bounds of the matrix on the row top and bottom
                    || nextRow >= matrix.Length //Check the Bottom
                    || nextColumn < 0
                    || nextColumn >= matrix[nextRow].Length //check the end of the column list
                    || matrix[nextRow][nextColumn] == '0' //if the item has already been visited
                )
                {
                    continue;
                }
                //if all the checks pass, then set item as visited, and add it to the stack to see if it has any 
                matrix[nextRow][nextColumn] = '0';
                stack.Push((nextRow, nextColumn));

            }

        }

        return 1;

    }







    /// <summary>
    /// Scans every cell of the grid and counts the islands.
    /// Each time a '1' is found it is the first cell of a new island,
    /// so the count goes up by 1 and <see cref="DFSWithStack"/> sinks the rest of that island.
    /// </summary>
    /// <param name="matrix">The grid of '1' (land) and '0' (water). It is changed to all '0's.</param>
    /// <returns>The number of islands in the grid.</returns>
    /// <remarks>This has the same shape as the LeetCode method NumIslands(char[][] grid).</remarks>
    public static int CountIslands(char[][] matrix)
    {
        int returnValue = 0;
        //we want to loop through each of the rows and columns looking for 1s that
        //indicate an Island.
        for (int row = 0; row < matrix.Length; row++)
        {
            for (int column = 0; column < matrix[row].Length; column++)
            {
                //as we find a one, we will pass the current row number
                //and the column number down to the DFS function to analyze
                //all of the elments around the
                /*
                       example [1] in the center and the function will look up and down around it
                       0 1 0
                       1[1]1
                       0 0 0
                */
                if (matrix[row][column] == '1')
                {
                    //DFSWithStack returns 1 after it sinks the island, so this adds 1 to the count
                    returnValue += DFSWithStack(matrix, row, column);
                }
            }
        }

        //the total number of islands found
        return returnValue;
    }

    public static int DFSWithStack(char[][] matrix, int startRow, int startColumn)
    {

        /*

            5 checks
            1. Start Row is less than 0
            or 2. Start Row is greater than the matrix row length
            or 3. Start Column is less than 0
            or 4. Start Column is greater than the rows column count
            or 5. Current cell is already 0
        */
        if
        (
            startRow < 0
            || startRow >= matrix.Length
            || startColumn < 0
            || startColumn >= matrix[startRow].Length
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

        //tuple array
        (int row, int column)[] directions = new (int, int)[]
        {
                    ( 1,  0),   // DOWN
                    (-1,  0),   // UP
                    ( 0,  1),   // RIGHT6
                    ( 0, -1)    // LEFT
        };

        //cells that have been sunk but whose neighbours have not been checked yet
        //add a stack here to reduce memory consumption
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
                    nextRow < 0
                    || nextRow >= matrix.Length
                    || nextColumn < 0
                    || nextColumn >= matrix[nextRow].Length
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
