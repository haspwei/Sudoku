namespace Sudoku
{
    public static class GridUtils
    {
        public const int GridSize = 9;
        public const int BoxSize = 3;

        public static void CopyGrid(int[,] source, int[,] target)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    target[row, col] = source[row, col];
                }
            }
        }

        public static bool IsValidPlacement(int[,] grid, int row, int col, int num)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (grid[row, i] == num)
                {
                    return false;
                }
            }

            for (int i = 0; i < GridSize; i++)
            {
                if (grid[i, col] == num)
                {
                    return false;
                }
            }

            int boxRowStart = row - row % BoxSize;
            int boxColStart = col - col % BoxSize;

            for (int i = 0; i < BoxSize; i++)
            {
                for (int j = 0; j < BoxSize; j++)
                {
                    if (grid[boxRowStart + i, boxColStart + j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static (int row, int col) FindEmptyCell(int[,] grid)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (grid[row, col] == 0)
                    {
                        return (row, col);
                    }
                }
            }

            return (-1, -1);
        }
    }
}
