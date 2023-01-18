namespace API.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}