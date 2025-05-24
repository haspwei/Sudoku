namespace Sudoku
{
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            if (QuitButton != null)
                QuitButton.Click += QuitButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            LoadSavedGame();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вийти з гри?", "Підтвердження",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void StartNewGame()
        {
            var difficulty = (DifficultyLevel)cmbDifficulty.SelectedIndex;
            var gameForm = new SudokuGame(difficulty) { Owner = this };
            this.Hide();
            gameForm.ShowDialog();
            this.Show();
        }

        private void LoadSavedGame()
        {
            try
            {
                var (currentGrid, puzzleGrid, solutionGrid, difficulty, notes) = SaveManager.LoadGame();
                var gameForm = new SudokuGame(difficulty) { Owner = this };
                gameForm.SetLoadedPuzzle(currentGrid, puzzleGrid, solutionGrid, notes);
                this.Hide();
                gameForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}