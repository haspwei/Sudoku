using Game;

namespace Sudoku
{
    public class GameStateManager
    {
        private int[,] _puzzleGrid = new int[9, 9]; 
        private int[,] _solutionGrid = new int[9, 9];
        private int[,] _currentGrid = new int[9, 9];
        private SudokuGameModeHandler _gameModeHandler;
        private DifficultyLevel _difficulty;
        private NotesManager _notesManager;
        private GameMode _currentMode = GameMode.Normal;
        private bool _isDisposed = false;
        public event Action<int[,]> OnGridUpdated;
        public event Action SolveCompletedEvent;
        public event Action ModeChangedEvent;

        public GameStateManager(NotesManager notesManager, DifficultyLevel difficulty)
        {
            _notesManager = notesManager;
            _difficulty = difficulty;
        }

        public void SetGameHandler(SudokuGameModeHandler handler)
        {
            _gameModeHandler = handler;
            _gameModeHandler.OnGridUpdated += HandleGridUpdated;
            _gameModeHandler.OnSolveCompleted += HandleSolveCompleted;
        }

        public DifficultyLevel Difficulty => _difficulty;
        public GameMode CurrentMode => _currentMode;

        public void SetGrids(int[,] puzzleGrid, int[,] solutionGrid, int[,] currentGrid)
        {
            if (_isDisposed) return;

            GridUtils.CopyGrid(puzzleGrid, _puzzleGrid);
            GridUtils.CopyGrid(solutionGrid, _solutionGrid);
            GridUtils.CopyGrid(currentGrid, _currentGrid);
        }

        public void ChangeGameMode(GameMode newMode)
        {
            if (_isDisposed || _currentMode == newMode)
                return;

            _currentMode = newMode;
            _gameModeHandler?.ChangeGameMode(newMode);
            ModeChangedEvent?.Invoke();
        }

        public bool SetUserValue(int row, int col, int number)
        {
            if (_isDisposed || _currentMode != GameMode.Normal)
                return false;

            if (_gameModeHandler.SetUserValue(row, col, number))
            {
                _currentGrid[row, col] = number;
                _notesManager.ClearCellNotes(row, col);
                return true;
            }
            return false;
        }

        public bool ClearUserValue(int row, int col)
        {
            if (_isDisposed || _currentMode != GameMode.Normal)
                return false;

            if (_gameModeHandler.ClearUserValue(row, col))
            {
                _currentGrid[row, col] = 0;
                _notesManager.ClearCellNotes(row, col);
                return true;
            }
            return false;
        }

        public List<int> GetHint(int row, int col)
        {
            if (_isDisposed || _currentMode != GameMode.Normal)
                return new List<int>();

            return _gameModeHandler.GetHint(row, col);
        }

        public void UpdateStepDelay(int delay)
        {
            if (_isDisposed || _gameModeHandler == null)
                return;

            _gameModeHandler.StepDelay = delay;
        }

        public void SaveGame()
        {
            if (_isDisposed)
                return;

            SaveManager.SaveGame(_currentGrid, _puzzleGrid, _solutionGrid,
                _difficulty, _notesManager.GetAllNotes());
        }

        public void StopAllProcesses()
        {
            _isDisposed = true;
            _gameModeHandler?.StopAllProcesses();
        }

        public void UnsubscribeAllEvents()
        {
            if (_gameModeHandler != null)
            {
                _gameModeHandler.OnGridUpdated -= HandleGridUpdated;
                _gameModeHandler.OnSolveCompleted -= HandleSolveCompleted;
            }

            OnGridUpdated = null;
            SolveCompletedEvent = null;
            ModeChangedEvent = null;
        }

        private void HandleGridUpdated(int[,] grid)
        {
            if (_isDisposed)
                return;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    _currentGrid[row, col] = grid[row, col];
                }
            }
            OnGridUpdated?.Invoke(grid);
        }

        private void HandleSolveCompleted(bool success)
        {
            if (_isDisposed)
                return;

            SolveCompletedEvent?.Invoke();
        }

        public int[,] PuzzleGrid => _puzzleGrid;
        public int[,] CurrentGrid => _currentGrid;
        public int[,] SolutionGrid => _solutionGrid;
    }
}