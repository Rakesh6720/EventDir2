using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class GetEventDTO
    {
        public int Id { get; set; }
        public string Name {get; set;} = string.Empty;
        public string Address1 {get; set;} = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public MemberDTO Organizer {get; set;}
        public ICollection<EventEnrollmentDTO> Enrollments { get; set; }
    }
}