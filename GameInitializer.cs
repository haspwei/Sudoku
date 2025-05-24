using Game;

namespace Sudoku
{
    public class GameInitializer
    {
        private readonly GameStateManager _gameState;
        private readonly NotesManager _notesManager;
        private readonly SudokuUIController _uiController;
        private readonly Panel _panelSudoku;

        private bool _isLoadedGame = false;
        private int[,] _loadedGrid = null;
        private int[,] _loadedPuzzleGrid = null;
        private int[,] _loadedSolutionGrid = null;
        private Dictionary<(int, int), HashSet<int>> _loadedNotes = null;

        public GameInitializer(
            GameStateManager gameState,
            NotesManager notesManager,
            SudokuUIController uiController,
            Panel panelSudoku)
        {
            _gameState = gameState;
            _notesManager = notesManager;
            _uiController = uiController;
            _panelSudoku = panelSudoku;
        }

        public void SetLoadedPuzzle(
            int[,] currentGrid,
            int[,] puzzleGrid,
            int[,] solutionGrid = null,
            Dictionary<(int, int), HashSet<int>> notes = null)
        {
            _isLoadedGame = true;
            _loadedGrid = new int[9, 9];
            _loadedPuzzleGrid = new int[9, 9];

            if (solutionGrid != null)
            {
                _loadedSolutionGrid = new int[9, 9];
                GridUtils.CopyGrid(solutionGrid, _loadedSolutionGrid);
            }

            GridUtils.CopyGrid(currentGrid, _loadedGrid);
            GridUtils.CopyGrid(puzzleGrid, _loadedPuzzleGrid);

            _loadedNotes = notes != null
                ? new Dictionary<(int, int), HashSet<int>>(notes)
                : null;
        }


        public void InitializeGame()
        {
            int[,] puzzleGrid = new int[9, 9];
            int[,] solutionGrid = new int[9, 9];
            int[,] currentGrid = new int[9, 9];

            if (_isLoadedGame && _loadedGrid != null && _loadedPuzzleGrid != null)
            {
                GridUtils.CopyGrid(_loadedPuzzleGrid, puzzleGrid);
                GridUtils.CopyGrid(_loadedGrid, currentGrid);

                if (_loadedSolutionGrid != null)
                {
                    GridUtils.CopyGrid(_loadedSolutionGrid, solutionGrid);
                }
                else
                {
                    var generator = new SudokuGenerator();
                    solutionGrid = generator.GetSolution();
                }

                _notesManager.Clear();
                if (_loadedNotes != null)
                {
                    foreach (var kvp in _loadedNotes)
                    {
                        foreach (var note in kvp.Value)
                        {
                            _notesManager.ToggleNote(kvp.Key.Item1, kvp.Key.Item2, note);
                        }
                    }
                }

                _isLoadedGame = false;
                _loadedGrid = null;
                _loadedPuzzleGrid = null;
                _loadedSolutionGrid = null;
                _loadedNotes = null;
            }
            else
            {
                var generator = new SudokuGenerator();
                puzzleGrid = generator.GeneratePuzzle(_gameState.Difficulty);
                solutionGrid = generator.GetSolution();
                GridUtils.CopyGrid(puzzleGrid, currentGrid);
                _notesManager.Clear();
            }

            _gameState.SetGrids(puzzleGrid, solutionGrid, currentGrid);

            var gameModeHandler = new SudokuGameModeHandler(puzzleGrid, solutionGrid);
            gameModeHandler.InitializeWithCurrentState(currentGrid);
            _gameState.SetGameHandler(gameModeHandler);

            _uiController.SetupSudokuGrid(_panelSudoku, puzzleGrid);
            _uiController.UpdateGridDisplay(
                currentGrid,
                puzzleGrid,
                currentGrid,
                _notesManager.GetAllNotes()
            );
            _uiController.UpdateAllNotesDisplay(
                puzzleGrid,
                currentGrid,
                _notesManager.GetAllNotes()
            );
        }
    }
}
