namespace DTOs
{
    public class IslandDTO
    {
        public int IslandID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Abbreviation {  get; set; } = string.Empty;
        public List<TurbineDTO> Turbines { get; set; } = [];
    }
}
