using Game;

namespace Sudoku
{
    public class EventHandlerManager
    {
        private readonly SudokuUIController _uiController;
        private readonly GameStateManager _gameState;
        private readonly NotesManager _notesManager;
        private readonly Form _form;


        public EventHandlerManager(
            SudokuUIController uiController,
            GameStateManager gameState,
            NotesManager notesManager,
            Form form)
        {
            _uiController = uiController;
            _gameState = gameState;
            _notesManager = notesManager;
            _form = form;
        }

        public void SetupEventHandlers(
            Panel panelNumbers,
            Button[] numberButtons,
            Button clearButton,
            Button notesButton,
            Button checkSolutionButton,
            Button saveButton,
            Button returnButton,
            Button hintButton,
            Button normalModeButton,
            Button stepModeButton,
            Button autoModeButton,
            TrackBar speedTrackBar)
        {
            foreach (var button in numberButtons)
            {
                button.Click += NumberButton_Click;
            }

            clearButton.Click += ClearButton_Click;
            notesButton.Click += NotesButton_Click;
            checkSolutionButton.Click += CheckSolutionButton_Click;
            saveButton.Click += SaveButton_Click;
            returnButton.Click += ReturnButton_Click;
            hintButton.Click += HintButton_Click;

            normalModeButton.Click += NormalModeButton_Click;
            stepModeButton.Click += StepModeButton_Click;
            autoModeButton.Click += AutoModeButton_Click;

            speedTrackBar.ValueChanged += SpeedTrackBar_ValueChanged;

            _gameState.ModeChangedEvent += () =>
            {
                UpdateUIForGameMode(
                    panelNumbers,
                    notesButton,
                    hintButton,
                    checkSolutionButton,
                    saveButton,
                    speedTrackBar,
                    normalModeButton,
                    stepModeButton,
                    autoModeButton
                );
            };

            _gameState.SolveCompletedEvent += () =>
            {
                ShowSolveCompletedMessage();
                _uiController.SetModeButtonsEnabled(true, normalModeButton, stepModeButton, autoModeButton);
                normalModeButton.PerformClick();
            };
        }

        private void UpdateUIForGameMode(
            Panel panelNumbers,
            Button notesButton,
            Button hintButton,
            Button checkSolutionButton,
            Button saveButton,
            TrackBar speedTrackBar,
            Button normalModeButton,
            Button stepModeButton,
            Button autoModeButton)
        {
            bool isNormalMode = _gameState.CurrentMode == GameMode.Normal;

            _uiController.UpdateNumbersPanelEnabled(panelNumbers, isNormalMode);

            notesButton.Enabled = isNormalMode;
            hintButton.Enabled = isNormalMode;
            checkSolutionButton.Enabled = isNormalMode;
            saveButton.Enabled = isNormalMode;
            speedTrackBar.Enabled = _gameState.CurrentMode == GameMode.StepByStep;

            if (_gameState.CurrentMode == GameMode.StepByStep || _gameState.CurrentMode == GameMode.AutoSolve)
            {
                _uiController.SetModeButtonsEnabled(false, normalModeButton, stepModeButton, autoModeButton);
            }
            else
            {
                _uiController.SetModeButtonsEnabled(true, normalModeButton, stepModeButton, autoModeButton);
            }

            _uiController.ClearSelection();
            _uiController.UpdateHighlights(_gameState.PuzzleGrid);
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            var (selectedRow, selectedCol) = _uiController.GetSelectedCell();
            if (selectedRow == -1 || selectedCol == -1 || _gameState.CurrentMode != GameMode.Normal)
                return;

            var button = (Button)sender;
            int number = int.Parse(button.Text);

            if (_uiController.IsNotesMode())
            {
                _notesManager.ToggleNote(selectedRow, selectedCol, number);
                _uiController.UpdateCellDisplay(
                    selectedRow,
                    selectedCol,
                    _gameState.PuzzleGrid,
                    _gameState.CurrentGrid,
                    _notesManager.GetAllNotes()
                );
            }
            else
            {
                if (_gameState.SetUserValue(selectedRow, selectedCol, number))
                {
                    _uiController.UpdateCellDisplay(
                        selectedRow,
                        selectedCol,
                        _gameState.PuzzleGrid,
                        _gameState.CurrentGrid,
                        _notesManager.GetAllNotes()
                    );
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            var (selectedRow, selectedCol) = _uiController.GetSelectedCell();
            if (selectedRow == -1 || selectedCol == -1 || _gameState.CurrentMode != GameMode.Normal)
                return;

            if (_gameState.ClearUserValue(selectedRow, selectedCol))
            {
                _uiController.UpdateCellDisplay(
                    selectedRow,
                    selectedCol,
                    _gameState.PuzzleGrid,
                    _gameState.CurrentGrid,
                    _notesManager.GetAllNotes()
                );
            }
        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            _uiController.ToggleNotesMode((Button)sender);
        }

        private void HintButton_Click(object sender, EventArgs e)
        {
            var (selectedRow, selectedCol) = _uiController.GetSelectedCell();
            if (selectedRow == -1 || selectedCol == -1 || _gameState.CurrentMode != GameMode.Normal)
                return;

            bool hasUserValue = _gameState.CurrentGrid[selectedRow, selectedCol] != 0 &&
                                _gameState.PuzzleGrid[selectedRow, selectedCol] == 0;

            List<int> possibleValues = _gameState.GetHint(selectedRow, selectedCol);

            if (possibleValues.Count > 0)
            {
                _notesManager.ClearCellNotes(selectedRow, selectedCol);

                if (hasUserValue)
                {
                    _gameState.ClearUserValue(selectedRow, selectedCol);
                }

                foreach (int value in possibleValues)
                {
                    _notesManager.ToggleNote(selectedRow, selectedCol, value);
                }

                _uiController.UpdateCellDisplay(
                    selectedRow,
                    selectedCol,
                    _gameState.PuzzleGrid,
                    _gameState.CurrentGrid,
                    _notesManager.GetAllNotes()
                );

                MessageBox.Show($"Знайдено {possibleValues.Count} можливих значень для цієї клітинки.",
                    "Підказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Немає можливих значень для цієї клітинки.",
                    "Підказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _gameState.SaveGame();
                MessageBox.Show("Гру успішно збережено!", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Повернутися до головного меню? Незбережений прогрес буде втрачено.",
                              "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _form.DialogResult = DialogResult.Abort;
                _form.Close();
            }
        }

        private void CheckSolutionButton_Click(object sender, EventArgs e)
        {
            _uiController.ShowSolutionResult(_gameState.CurrentGrid, _gameState.SolutionGrid);
        }

        private void NormalModeButton_Click(object sender, EventArgs e)
        {
            if (_gameState.CurrentMode != GameMode.Normal)
            {
                _gameState.ChangeGameMode(GameMode.Normal);
                _uiController.SetActiveModeButton((Button)sender);
            }
        }

        private void StepModeButton_Click(object sender, EventArgs e)
        {
            if (_gameState.CurrentMode != GameMode.StepByStep)
            {
                _gameState.ChangeGameMode(GameMode.StepByStep);
                _uiController.SetActiveModeButton((Button)sender);
            }
        }

        private void AutoModeButton_Click(object sender, EventArgs e)
        {
            if (_gameState.CurrentMode != GameMode.AutoSolve)
            {
                _gameState.ChangeGameMode(GameMode.AutoSolve);
                _uiController.SetActiveModeButton((Button)sender);
            }
        }

        private void SpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            int delayMs = 500 - ((trackBar.Value - 1) * 490 / 9);

            _gameState.UpdateStepDelay(delayMs);
        }

        private void ShowSolveCompletedMessage()
        {
            MessageBox.Show("Судоку успішно вирішено!", "Вирішення",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}