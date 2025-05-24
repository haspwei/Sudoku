using System.Text;

namespace Sudoku
{
    public class SudokuUIController
    {
        private SudokuGame _gameForm;
        private Button[,] _cells = new Button[9, 9];
        private bool _highlightingActive = false;
        private int _selectedRow = -1;
        private int _selectedCol = -1;
        private bool _notesMode = false;
        private Button _activeModeButton = null;

        private const int CellSize = 89;
        private const int BlockSpacing = 1;
        private readonly Font MainCellFont = new Font("Yu Gothic", 16, FontStyle.Bold);
        private readonly Font UserCellFont = new Font("Yu Gothic", 16, FontStyle.Regular);
        private readonly Font NotesCellFont = new Font("Yu Gothic", 9, FontStyle.Regular);
        private readonly Color HintColor = Color.MediumBlue;
        private readonly Color OriginalCellColor = Color.LightSteelBlue;
        private readonly Color EmptyCellColor = Color.White;
        private readonly Color SelectedCellColor = Color.LightBlue;
        private readonly Color HighlightedRowColColor = Color.Lavender;
        private readonly Color HoverColor = Color.LightCyan;
        private readonly Color ActiveModeColor = Color.LightGreen;
        private readonly Color NotesModeActiveColor = Color.LightPink;
        private readonly Color NotesModeInactiveColor = Color.LightSteelBlue;
        private readonly Color NotesTextColor = Color.DarkSlateBlue;
        private readonly Color CorrectCellColor = Color.LightGreen;
        private readonly Color IncorrectCellColor = Color.LightCoral;

        public SudokuUIController(SudokuGame gameForm)
        {
            _gameForm = gameForm;
        }

        public void SetupSudokuGrid(Panel panelSudoku, int[,] puzzleGrid)
        {
            panelSudoku.Controls.Clear();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var cell = new Button();
                    cell.Size = new Size(CellSize, CellSize);
                    cell.Location = new Point(
                        col * CellSize + (col / 3) * BlockSpacing,
                        row * CellSize + (row / 3) * BlockSpacing
                    );
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Font = MainCellFont;
                    cell.Tag = new Point(row, col);
                    cell.BackColor = puzzleGrid[row, col] != 0 ? OriginalCellColor : EmptyCellColor;
                    cell.TextAlign = ContentAlignment.MiddleCenter;
                    cell.FlatAppearance.BorderColor = Color.Gray;
                    cell.FlatAppearance.BorderSize = 1;
                    cell.Padding = new Padding(0);

                    cell.Click += (sender, e) => Cell_Click(sender, e, puzzleGrid);
                    cell.MouseEnter += (sender, e) => Cell_MouseEnter(sender, e, puzzleGrid);
                    cell.MouseLeave += (sender, e) => Cell_MouseLeave(sender, e, puzzleGrid);

                    panelSudoku.Controls.Add(cell);
                    _cells[row, col] = cell;
                }
            }
        }

        public void UpdateAllNotesDisplay(int[,] puzzleGrid, int[,] currentGrid, Dictionary<(int, int), HashSet<int>> notes)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    UpdateCellDisplay(row, col, puzzleGrid, currentGrid, notes);
                }
            }
        }

        public void UpdateCellDisplay(int row, int col, int[,] puzzleGrid, int[,] currentGrid, Dictionary<(int, int), HashSet<int>> notes)
        {
            if (puzzleGrid[row, col] != 0) 
            {
                _cells[row, col].Text = puzzleGrid[row, col].ToString();
                _cells[row, col].Font = MainCellFont;
                _cells[row, col].ForeColor = Color.Black;
                _cells[row, col].TextAlign = ContentAlignment.MiddleCenter;
                return;
            }

            if (currentGrid[row, col] != 0) 
            {
                _cells[row, col].Text = currentGrid[row, col].ToString();
                _cells[row, col].Font = UserCellFont;
                _cells[row, col].ForeColor = Color.Black;
                _cells[row, col].TextAlign = ContentAlignment.MiddleCenter;
                return;
            }

            if (notes.ContainsKey((row, col)) && notes[(row, col)].Count > 0) 
            {
                var notesList = notes[(row, col)].OrderBy(n => n).ToList();
                var noteText = new StringBuilder();

                for (int i = 0; i < notesList.Count; i++)
                {
                    noteText.Append(notesList[i]);
                    if ((i + 1) % 3 == 0 && i < notesList.Count - 1)
                        noteText.AppendLine();
                    else if (i < notesList.Count - 1)
                        noteText.Append(" ");
                }

                _cells[row, col].Text = noteText.ToString();
                _cells[row, col].Font = NotesCellFont;
                _cells[row, col].ForeColor = NotesTextColor;
                _cells[row, col].TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                _cells[row, col].Text = "";
                _cells[row, col].TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        public void UpdateGridDisplay(int[,] grid, int[,] puzzleGrid, int[,] currentGrid, Dictionary<(int, int), HashSet<int>> notes)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    UpdateCellDisplay(row, col, puzzleGrid, currentGrid, notes);
                }
            }
        }

        public void SetHintValue(int row, int col, int hintValue)
        {
            _cells[row, col].Text = hintValue.ToString();
            _cells[row, col].Font = UserCellFont;
            _cells[row, col].ForeColor = HintColor;
        }

        public void ToggleNotesMode(Button notesButton)
        {
            _notesMode = !_notesMode;
            notesButton.BackColor = _notesMode ? NotesModeActiveColor : NotesModeInactiveColor;
        }

        public bool IsNotesMode()
        {
            return _notesMode;
        }

        public void UpdateHighlights(int[,] puzzleGrid)
        {
            _highlightingActive = false;

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    _cells[r, c].BackColor = puzzleGrid[r, c] != 0 ? OriginalCellColor : EmptyCellColor;
                }
            }

            if (_selectedRow != -1 && _selectedCol != -1)
            {
                for (int i = 0; i < 9; i++)
                {
                    _cells[_selectedRow, i].BackColor = HighlightedRowColColor;
                    _cells[i, _selectedCol].BackColor = HighlightedRowColColor;
                }
                _cells[_selectedRow, _selectedCol].BackColor = SelectedCellColor;
            }
        }

        public void ClearSelection()
        {
            if (_selectedRow != -1 && _selectedCol != -1)
            {
                _cells[_selectedRow, _selectedCol].FlatAppearance.BorderColor = Color.Gray;
                _cells[_selectedRow, _selectedCol].FlatAppearance.BorderSize = 1;
            }
        }

        public void SetModeButtonsEnabled(bool enabled, params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.Enabled = enabled;
            }

            if (_activeModeButton != null && !enabled)
            {
                _activeModeButton.Enabled = false;
            }
        }

        public void SetActiveModeButton(Button button)
        {
            if (_activeModeButton != null)
            {
                _activeModeButton.BackColor = NotesModeInactiveColor;
            }

            _activeModeButton = button;
            _activeModeButton.BackColor = ActiveModeColor;
            _activeModeButton.Enabled = false;
        }

        public void UpdateNumbersPanelEnabled(Panel numbersPanel, bool enabled)
        {
            foreach (Control control in numbersPanel.Controls)
            {
                control.Enabled = enabled;
            }
        }

        public void ShowSolutionResult(int[,] currentGrid, int[,] solutionGrid)
        {
            bool isComplete = true;
            bool isCorrect = true;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (currentGrid[row, col] == 0)
                    {
                        isComplete = false;
                        isCorrect = false;
                        break;
                    }

                    if (currentGrid[row, col] != solutionGrid[row, col])
                    {
                        isCorrect = false;
                    }
                }

                if (!isComplete || !isCorrect)
                    break;
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (currentGrid[row, col] != 0)
                    {
                        _cells[row, col].BackColor = currentGrid[row, col] == solutionGrid[row, col]
                            ? CorrectCellColor
                            : IncorrectCellColor;
                    }
                }
            }

            _highlightingActive = true;

            if (!isComplete)
            {
                MessageBox.Show("Судоку не повністю заповнене!", "Попередження",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (isCorrect)
            {
                MessageBox.Show("Вітаємо! Ви правильно вирішили судоку!", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("У рішенні є помилки.", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public (int, int) GetSelectedCell()
        {
            return (_selectedRow, _selectedCol);
        }

        private void Cell_Click(object sender, EventArgs e, int[,] puzzleGrid)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;
            int row = position.X;
            int col = position.Y;

            if (puzzleGrid[row, col] != 0)
                return;

            _selectedRow = row;
            _selectedCol = col;
            UpdateHighlights(puzzleGrid);
        }

        private void Cell_MouseEnter(object sender, EventArgs e, int[,] puzzleGrid)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;
            int row = position.X;
            int col = position.Y;

            if ((row == _selectedRow && col == _selectedCol) ||
                (_selectedRow != -1 && (row == _selectedRow || col == _selectedCol)))
                return;

            if (!_highlightingActive)
            {
                button.BackColor = HoverColor;
            }
        }

        private void Cell_MouseLeave(object sender, EventArgs e, int[,] puzzleGrid)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;
            int row = position.X;
            int col = position.Y;

            if ((row == _selectedRow && col == _selectedCol) ||
                (_selectedRow != -1 && (row == _selectedRow || col == _selectedCol)))
                return;

            if (!_highlightingActive)
            {
                button.BackColor = puzzleGrid[row, col] != 0 ? OriginalCellColor : EmptyCellColor;
            }

        }
    }
}
