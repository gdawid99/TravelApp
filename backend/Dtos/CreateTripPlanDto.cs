namespace TravelApp.Dtos
{
    public class CreateTripPlanDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }

    }
}