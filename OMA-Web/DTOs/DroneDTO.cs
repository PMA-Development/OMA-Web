namespace DTOs
{
    public class DroneDTO
    {
        public int DroneID { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Available { get; set; }
        public int TurbineID { get; set; }
    }
}
