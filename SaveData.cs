namespace Sudoku
{
    public class SaveData
    {
        public int[][] CurrentGrid { get; set; }
        public int[][] PuzzleGrid { get; set; }
        public int[][] SolutionGrid { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public Dictionary<string, int[]> Notes { get; set; }

        public SaveData()
        {
            Notes = new Dictionary<string, int[]>();
        }

        public SaveData(int[,] currentGrid, int[,] puzzleGrid, int[,] solutionGrid,
                       DifficultyLevel difficulty, Dictionary<(int, int), HashSet<int>> notes)
        {
            CurrentGrid = GridConverter.To2DArray(currentGrid);
            PuzzleGrid = GridConverter.To2DArray(puzzleGrid);
            SolutionGrid = GridConverter.To2DArray(solutionGrid);
            Difficulty = difficulty;
            Notes = NotesConverter.ToSaveable(notes);
        }

        public int[,] GetCurrentGrid() => GridConverter.ToGrid(CurrentGrid);
        public int[,] GetPuzzleGrid() => GridConverter.ToGrid(PuzzleGrid);
        public int[,] GetSolutionGrid() => GridConverter.ToGrid(SolutionGrid);
        public Dictionary<(int, int), HashSet<int>> GetNotes() => NotesConverter.FromSaveable(Notes);
    }
}
