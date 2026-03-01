namespace TravelApp.Dtos
{
    public class UserDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LoginDate { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
    }
}
