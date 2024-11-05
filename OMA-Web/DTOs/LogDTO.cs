namespace DTOs
{
    public class LogDTO
    {
        public int LogID { get; set; }
        public DateTime Time { get; set; }
        public string Severity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserID { get; set; }
    }
}
