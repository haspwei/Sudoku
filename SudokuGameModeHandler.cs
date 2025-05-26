using Sudoku;

namespace Game
{
    public enum GameMode
    {
        Normal,
        AutoSolve,
        StepByStep
    }

    public class SudokuGameModeHandler
    {
        private readonly int[,] _puzzleGrid;
        private readonly int[,] _solutionGrid;
        private int[,] _workingGrid;
        public event Action<int[,]> OnGridUpdated;
        public event Action<bool> OnSolveCompleted;
        private int _stepDelay = 100;
        private CancellationTokenSource _cancellationTokenSource;
        private GameMode _currentMode;
        private bool _isDisposed = false;

        public SudokuGameModeHandler(int[,] puzzleGrid, int[,] solutionGrid)
        {
            _puzzleGrid = puzzleGrid;
            _solutionGrid = solutionGrid;
            _workingGrid = new int[GridUtils.GridSize, GridUtils.GridSize];
            GridUtils.CopyGrid(puzzleGrid, _workingGrid);
            _currentMode = GameMode.Normal;
        }

        public void InitializeWithCurrentState(int[,] currentGrid)
        {
            if (_isDisposed) return;
            GridUtils.CopyGrid(currentGrid, _workingGrid);
        }

        public int StepDelay
        {
            get => _stepDelay;
            set => _stepDelay = Math.Max(10, Math.Min(1000, value));
        }

        public GameMode CurrentMode => _currentMode;

        public void StopAllProcesses()
        {
            _isDisposed = true;

            if (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public void ChangeGameMode(GameMode newMode)
        {
            if (_isDisposed) return;

            if ((_currentMode == GameMode.AutoSolve || _currentMode == GameMode.StepByStep) &&
                _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }

            _currentMode = newMode;

            if (newMode == GameMode.Normal)
            {
                OnGridUpdated?.Invoke(_workingGrid);
                return;
            }

            if (newMode == GameMode.AutoSolve || newMode == GameMode.StepByStep)
            {
                StartSolving();
            }
        }

        private async void StartSolving()
        {
            if (_isDisposed) return;

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                bool success;

                if (_currentMode == GameMode.AutoSolve)
                {
                    GridUtils.CopyGrid(_puzzleGrid, _workingGrid);
                    success = await Task.Run(() =>
                        AutoSolve(_workingGrid, _cancellationTokenSource.Token));
                    if (success && !_cancellationTokenSource.Token.IsCancellationRequested && !_isDisposed)
                    {
                        OnGridUpdated?.Invoke(_workingGrid);
                    }
                }
                else if (_currentMode == GameMode.StepByStep)
                {
                    GridUtils.CopyGrid(_puzzleGrid, _workingGrid);
                    success = await StepByStepSolve(0, 0);
                }
                else
                {
                    return;
                }

                if (!_cancellationTokenSource.Token.IsCancellationRequested && !_isDisposed)
                {
                    OnSolveCompleted?.Invoke(success);
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при вирішенні судоку: {ex.Message}");
                if (!_isDisposed)
                {
                    OnSolveCompleted?.Invoke(false);
                }
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private bool AutoSolve(int[,] grid, CancellationToken cancellationToken)
        {
            (int row, int col) = GridUtils.FindEmptyCell(grid);

            if (row == -1 && col == -1)
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (cancellationToken.IsCancellationRequested || _isDisposed)
                {
                    throw new OperationCanceledException();
                }

                if (GridUtils.IsValidPlacement(grid, row, col, num))
                {
                    grid[row, col] = num;

                    if (AutoSolve(grid, cancellationToken))
                    {
                        return true;
                    }

                    grid[row, col] = 0;
                }
            }

            return false;
        }

        private async Task<bool> StepByStepSolve(int row, int col)
        {
            if (_isDisposed) return false;

            bool foundEmpty = false;

            for (int r = row; r < 9; r++)
            {
                for (int c = (r == row ? col : 0); c < 9; c++)
                {
                    if (_workingGrid[r, c] == 0)
                    {
                        row = r;
                        col = c;
                        foundEmpty = true;
                        break;
                    }
                }

                if (foundEmpty)
                {
                    break;
                }
            }

            if (!foundEmpty)
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (_cancellationTokenSource?.Token.IsCancellationRequested == true || _isDisposed)
                {
                    throw new OperationCanceledException();
                }

                if (GridUtils.IsValidPlacement(_workingGrid, row, col, num))
                {
                    _workingGrid[row, col] = num;

                    if (!_isDisposed)
                    {
                        OnGridUpdated?.Invoke(_workingGrid);
                    }

                    try
                    {
                        await Task.Delay(_stepDelay, _cancellationTokenSource?.Token ?? CancellationToken.None);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }

                    if (await StepByStepSolve(row, col))
                    {
                        return true;
                    }

                    if (_cancellationTokenSource?.Token.IsCancellationRequested == true || _isDisposed)
                    {
                        throw new OperationCanceledException();
                    }

                    _workingGrid[row, col] = 0;

                    if (!_isDisposed)
                    {
                        OnGridUpdated?.Invoke(_workingGrid);
                    }

                    try
                    {
                        await Task.Delay(_stepDelay, _cancellationTokenSource?.Token ?? CancellationToken.None);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                }
            }

            return false;
        }

        public bool SetUserValue(int row, int col, int value)
        {
            if (_isDisposed || _currentMode != GameMode.Normal)
            {
                return false;
            }

            if (_puzzleGrid[row, col] != 0)
            {
                return false;
            }

            _workingGrid[row, col] = value;
            OnGridUpdated?.Invoke(_workingGrid);

            if (IsSudokuCompleted())
            {
                OnSolveCompleted?.Invoke(true);
            }
            return true;
        }

        public bool ClearUserValue(int row, int col)
        {
            if (_isDisposed || _currentMode != GameMode.Normal)
            {
                return false;
            }

            if (_puzzleGrid[row, col] != 0)
            {
                return false;
            }

            _workingGrid[row, col] = 0;
            OnGridUpdated?.Invoke(_workingGrid);

            return true;
        }

        private bool IsSudokuCompleted()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (_workingGrid[row, col] == 0 || _workingGrid[row, col] != _solutionGrid[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<int> GetHint(int row, int col)
        {
            if (_isDisposed || _currentMode != GameMode.Normal || _puzzleGrid[row, col] != 0)
            {
                return new List<int>();
            }

            int[,] tempGrid = new int[GridUtils.GridSize, GridUtils.GridSize];
            GridUtils.CopyGrid(_workingGrid, tempGrid);

            for (int r = 0; r < GridUtils.GridSize; r++)
            {
                for (int c = 0; c < GridUtils.GridSize; c++)
                {
                    if (tempGrid[r, c] != 0 && tempGrid[r, c] != _solutionGrid[r, c])
                    {
                        tempGrid[r, c] = 0;
                    }
                }
            }

            tempGrid[row, col] = 0;

            List<int> possibleValues = new List<int>();
            for (int num = 1; num <= 9; num++)
            {
                if (GridUtils.IsValidPlacement(tempGrid, row, col, num))
                {
                    possibleValues.Add(num);
                }
            }

            return possibleValues;
        }
    }
}
