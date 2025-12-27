namespace TipTapToe.Models
{
    // Class for logging user key inputs
    public class LogItem(string key, string timestamp, bool? result)
    {
        public string Key { get; set; } = key;
        public string Timestamp { get; set; } = timestamp;
        public bool? Result { get; set; } = result;

        public override string ToString()
        {
            return $"Key: {Key}, " + $"Timestamp: {Timestamp}, " + $"Result: {(Result != null ? Result : "null")}";
        }
    }
}