namespace Sudoku
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public class SudokuGenerator
    {
        private readonly Random _random;

        private int[,] _grid;
        private int[,] _solvedGrid;

        public SudokuGenerator()
        {
            _random = new Random();
            _grid = new int[GridUtils.GridSize, GridUtils.GridSize];
            _solvedGrid = new int[GridUtils.GridSize, GridUtils.GridSize];
        }

        public int[,] GeneratePuzzle(DifficultyLevel difficulty)
        {
            ClearGrid();
            GenerateCompleteSolution();
            GridUtils.CopyGrid(_grid, _solvedGrid);
            RemoveNumbers(difficulty);

            return _grid;
        }

        private void ClearGrid()
        {
            for (int row = 0; row < GridUtils.GridSize; row++)
            {
                for (int col = 0; col < GridUtils.GridSize; col++)
                {
                    _grid[row, col] = 0;
                }
            }
        }

        private bool GenerateCompleteSolution()
        {
            return FillGrid(0, 0);
        }

        private bool FillGrid(int row, int col)
        {
            if (row == GridUtils.GridSize)
            {
                row = 0;
                col++;
                if (col == GridUtils.GridSize)
                {
                    return true; 
                }
            }

            if (_grid[row, col] != 0)
            {
                return FillGrid(row + 1, col);
            }

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ShuffleList(numbers);

            foreach (int num in numbers)
            {
                if (GridUtils.IsValidPlacement(_grid, row, col, num))
                {
                    _grid[row, col] = num;

                    if (FillGrid(row + 1, col))
                    {
                        return true;
                    }

                    _grid[row, col] = 0;
                }
            }
            return false;
        }

        private void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void RemoveNumbers(DifficultyLevel difficulty)
        {
            int numbersToRemove;

            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    numbersToRemove = 40; 
                    break;
                case DifficultyLevel.Medium:
                    numbersToRemove = 50; 
                    break;
                case DifficultyLevel.Hard:
                    numbersToRemove = 60; 
                    break;
                default:
                    numbersToRemove = 45;
                    break;
            }

            List<(int row, int col)> positions = new List<(int, int)>();
            for (int row = 0; row < GridUtils.GridSize; row++)
            {
                for (int col = 0; col < GridUtils.GridSize; col++)
                {
                    positions.Add((row, col));
                }
            }

            ShuffleList(positions);

            int removed = 0;
            foreach (var (row, col) in positions)
            {
                if (removed >= numbersToRemove)
                {
                    break;
                }

                int temp = _grid[row, col];
                _grid[row, col] = 0;

                if (!HasUniqueSolution())
                {
                    _grid[row, col] = temp;
                    continue;
                }

                removed++;
            }
        }

        private bool HasUniqueSolution()
        {
            int[,] tempGrid = new int[GridUtils.GridSize, GridUtils.GridSize];
            GridUtils.CopyGrid(_grid, tempGrid);
            int solutionCount = 0;
            CountSolutions(tempGrid, 0, 0, ref solutionCount);

            return solutionCount == 1;
        }

        private void CountSolutions(int[,] grid, int row, int col, ref int count)
        {
            if (count > 1)
            {
                return;
            }

            if (row == GridUtils.GridSize)
            {
                count++;
                return;
            }

            int nextRow = (col == GridUtils.GridSize - 1) ? row + 1 : row;
            int nextCol = (col == GridUtils.GridSize - 1) ? 0 : col + 1;

            if (grid[row, col] != 0)
            {
                CountSolutions(grid, nextRow, nextCol, ref count);
                return;
            }

            for (int num = 1; num <= GridUtils.GridSize; num++)
            {
                if (GridUtils.IsValidPlacement(grid, row, col, num))
                {
                    grid[row, col] = num;
                    CountSolutions(grid, nextRow, nextCol, ref count);

                    if (count > 1)
                    {
                        return;
                    }

                    grid[row, col] = 0;
                }
            }
        }

        public int[,] GetSolution()
        {
            return _solvedGrid;
        }
    }
}
