namespace Sudoku
{
    public class NotesManager
    {
        private Dictionary<(int, int), HashSet<int>> _notes = new Dictionary<(int, int), HashSet<int>>();

        public event Action<int, int> OnNotesChanged;

        public NotesManager()
        {
            _notes = new Dictionary<(int, int), HashSet<int>>();
        }

        public NotesManager(Dictionary<(int, int), HashSet<int>> notes)
        {
            _notes = new Dictionary<(int, int), HashSet<int>>();
            if (notes != null)
            {
                foreach (var kvp in notes)
                {
                    _notes[kvp.Key] = new HashSet<int>(kvp.Value);
                }
            }
        }

        public void Clear()
        {
            _notes.Clear();
        }

        public Dictionary<(int, int), HashSet<int>> GetAllNotes()
        {
            var result = new Dictionary<(int, int), HashSet<int>>();
            foreach (var kvp in _notes)
            {
                result[kvp.Key] = new HashSet<int>(kvp.Value);
            }
            return result;
        }

        public HashSet<int> GetCellNotes(int row, int col)
        {
            if (_notes.ContainsKey((row, col)))
            {
                return new HashSet<int>(_notes[(row, col)]);
            }
            return new HashSet<int>();
        }

        public bool HasNotes(int row, int col)
        {
            return _notes.ContainsKey((row, col)) && _notes[(row, col)].Count > 0;
        }

        public void ToggleNote(int row, int col, int number)
        {
            if (!_notes.ContainsKey((row, col)))
            {
                _notes[(row, col)] = new HashSet<int>();
            }

            if (_notes[(row, col)].Contains(number))
            {
                _notes[(row, col)].Remove(number);
            }
            else
            {
                _notes[(row, col)].Add(number);
            }

            if (_notes[(row, col)].Count == 0)
            {
                _notes.Remove((row, col));
            }

            OnNotesChanged?.Invoke(row, col);
        }

        public void ClearCellNotes(int row, int col)
        {
            if (_notes.ContainsKey((row, col)))
            {
                _notes.Remove((row, col));
                OnNotesChanged?.Invoke(row, col);
            }
        }
    }
}
