namespace TravelApp.Dtos
{
    public class TripPlanPlaceDataDto
    {
        public string Name { get; set; }
        public string? Note { get; set; }
        public DateTime VisitDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
