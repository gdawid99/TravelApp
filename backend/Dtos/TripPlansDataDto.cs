namespace TravelApp.Dtos
{
    public class TripPlansDataDto
    {
        public Guid TripPlanId { get; set; }
        public string Title { get; set; }
        public IEnumerable<TripPlanPlaceDataDto> Places {  get; set; }
    }
}
