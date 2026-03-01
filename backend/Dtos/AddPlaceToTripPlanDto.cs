namespace TravelApp.Dtos
{
    public class AddPlaceToTripPlanDto
    {
        public Guid TripPlanId { get; set; }
        public string OsmId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
