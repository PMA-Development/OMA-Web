namespace DTOs
{
    public class TelemetryDTO
    {
        public int TelemetryID { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Acceleration { get; set; }
        public double Vibrations { get; set; }
        public double Rotations { get; set; }
    }
}
