namespace TipTapToe.Models
{
    // Struct for capturing user key inputs
    public struct KeyInput
    {
        public char? Char { get; set; }
        public ConsoleKey? Key { get; set;}

        public KeyInput(char c)
        {
            Char = c;
            Key = null;
        }
        public KeyInput(ConsoleKey k)
        {
            Char = null;
            Key = k;
        }

        public readonly override string ToString()
        {
            return Char?.ToString() ?? Key?.ToString() ?? string.Empty;
        }
    }
}