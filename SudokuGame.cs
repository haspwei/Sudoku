namespace Sudoku
{
    public partial class SudokuGame : Form
    {
        private GameStateManager _gameStateManager;
        private EventHandlerManager _eventHandlerManager;
        private GameInitializer _gameInitializer;
        private NotesManager _notesManager;

        private SudokuUIController _uiController;

        private bool _isFormClosing = false;

        public SudokuGame(DifficultyLevel difficulty)
        {
            InitializeComponent();

            _notesManager = new NotesManager();
            _uiController = new SudokuUIController(this);
            _gameStateManager = new GameStateManager(_notesManager, difficulty);

            _uiController.SetActiveModeButton(btnNormalMode);

            _eventHandlerManager = new EventHandlerManager(_uiController, _gameStateManager, _notesManager, this);
            _gameInitializer = new GameInitializer(_gameStateManager, _notesManager, _uiController, panelSudoku);

            SetupEventHandlers();

            _gameInitializer.InitializeGame();

            FormClosing += SudokuGame_FormClosing;
        }

        private void SudokuGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isFormClosing = true;
            _gameStateManager?.StopAllProcesses();
            _gameStateManager?.UnsubscribeAllEvents();
        }

        private void SetupEventHandlers()
        {
 
            Button[] numberButtons = new Button[]
            {
                Button1, Button2, Button3, Button4, Button5,
                Button6, Button7, Button8, Button9
            };

            _eventHandlerManager.SetupEventHandlers(
                panelNumbers,
                numberButtons,
                ClearButton,
                NotesButton,
                CheckSolutionButton,
                SaveButton,
                ReturnButton,
                HintButton,
                btnNormalMode,
                btnStepMode,
                btnAutoMode,
                trackBarSpeed
            );

            _gameStateManager.OnGridUpdated += UpdateGridDisplay;
        }

        public void SetLoadedPuzzle(
            int[,] currentGrid,
            int[,] puzzleGrid,
            int[,] solutionGrid = null,
            Dictionary<(int, int), HashSet<int>> notes = null)
        {
            _gameInitializer.SetLoadedPuzzle(currentGrid, puzzleGrid, solutionGrid, notes);
            _gameInitializer.InitializeGame();
        }

        private void UpdateGridDisplay(int[,] grid)
        {

            if (_isFormClosing || this.IsDisposed)
                return;

            try
            {
 
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => UpdateGridDisplaySafe(grid)));
                }
                else
                {
                    UpdateGridDisplaySafe(grid);
                }
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private void UpdateGridDisplaySafe(int[,] grid)
        {
            if (_isFormClosing || this.IsDisposed)
                return;

            _uiController.UpdateGridDisplay(
                grid,
                _gameStateManager.PuzzleGrid,
                _gameStateManager.CurrentGrid,
                _notesManager.GetAllNotes()
            );
        }

        public bool IsFormClosing => _isFormClosing;
    }
}