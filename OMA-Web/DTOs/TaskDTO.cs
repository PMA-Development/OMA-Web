namespace DTOs
{
    public class TaskDTO
    {
        public int TaskID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Level { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TurbineID { get; set; }
    }
}
