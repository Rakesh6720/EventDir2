using API.Entities;

namespace API.DTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public MemberDTO Member {get; set;}
        public EventDTO Event {get; set;}
    }
}