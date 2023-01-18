using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly EventContext _context;

        public MembersController(EventContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMemberDTO>>> GetMembers()
        {
            var members = await _context.Members.Include(m => m.Enrollments).ThenInclude(en => en.Event).ToListAsync();

            List<GetMemberDTO> memberDTOs = new List<GetMemberDTO>();

            foreach (var member in members) 
            {
                GetMemberDTO memberDTO = new GetMemberDTO
                {
                    Id = member.Id,
                    Username = member.Username,
                    Email = member.Email,
                    Enrollments = member.Enrollments.Select(item => new MemberEnrollmentDTO
                    {
                        Id = item.Id,
                        Event = new EventDTO{Id = item.Event.Id, Name = item.Event.Name}
                    }).ToList()
                };

                memberDTOs.Add(memberDTO);
            }

            return Ok(memberDTOs);
        }
    }
}