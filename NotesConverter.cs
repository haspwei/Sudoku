namespace Sudoku
{
    public static class NotesConverter
    {
        public static Dictionary<string, int[]> ToSaveable(Dictionary<(int, int), HashSet<int>> notes)
        {
            var result = new Dictionary<string, int[]>();
            foreach (var kvp in notes)
            {
                string key = $"{kvp.Key.Item1},{kvp.Key.Item2}";
                result[key] = kvp.Value.ToArray();
            }
            return result;
        }

        public static Dictionary<(int, int), HashSet<int>> FromSaveable(Dictionary<string, int[]> notes)
        {
            var result = new Dictionary<(int, int), HashSet<int>>();
            if (notes != null)
            {
                foreach (var kvp in notes)
                {
                    var parts = kvp.Key.Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int row) &&
                        int.TryParse(parts[1], out int col))
                    {
                        result[(row, col)] = new HashSet<int>(kvp.Value);
                    }
                }
            }
            return result;
        }
    }
}
