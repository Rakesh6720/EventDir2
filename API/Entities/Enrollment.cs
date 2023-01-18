namespace API.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int MemberId { get; set; }
        public Event Event { get; set; }
        public Member Member { get; set; }
    }
}