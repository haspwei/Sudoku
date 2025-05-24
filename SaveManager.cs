using System.Text.Json;

namespace Sudoku
{
    public static class SaveManager
    {
        private static string SavePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "SudokuSaves",
            "save.json");

        public static void SaveGame(int[,] currentGrid, int[,] puzzleGrid, int[,] solutionGrid,
                                  DifficultyLevel difficulty, Dictionary<(int, int), HashSet<int>> notes)
        {
            try
            {
                var saveData = new SaveData(currentGrid, puzzleGrid, solutionGrid, difficulty, notes);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(saveData, options);

                Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
                File.WriteAllText(SavePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка збереження: {ex.Message}");
            }
        }

        public static (int[,] currentGrid, int[,] puzzleGrid, int[,] solutionGrid,
                      DifficultyLevel difficulty, Dictionary<(int, int), HashSet<int>> notes) LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                throw new FileNotFoundException("Збережених ігор не знайдено");
            }

            try
            {
                string json = File.ReadAllText(SavePath);
                var saveData = JsonSerializer.Deserialize<SaveData>(json);
                return (saveData.GetCurrentGrid(), saveData.GetPuzzleGrid(),
                        saveData.GetSolutionGrid(), saveData.Difficulty, saveData.GetNotes());
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка завантаження: {ex.Message}");
            }
        }
    }
}