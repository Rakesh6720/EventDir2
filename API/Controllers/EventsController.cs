using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventContext _context;
        public EventsController(EventContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<GetEventDTO>>> GetAllEvents()
        {
            var events = await _context.Events.Include(e => e.Enrollments).ThenInclude(en => en.Member).ToListAsync();
            List<GetEventDTO> eventDTOs = new();

            foreach (var ev in events)
            {
                var eventDTO = new GetEventDTO
                {
                    Id = ev.Id,
                    Name = ev.Name,
                    Address1 = ev.Address1,
                    Address2 = ev.Address2,
                    City = ev.City,
                    State = ev.State,
                    Zip = ev.Zip,
                    Description = ev.Description,
                    ImageURL = ev.ImageURL,
                    //Organizer = new MemberDTO{Id = ev.Organizer.Id, Username = ev.Organizer.Username, Email = ev.Organizer.Email},
                    // Enrollments = ev.Enrollments.Select(item => new EventEnrollmentDTO
                    // {
                    //     Id = item.Id,
                    //     Member = new MemberDTO{Id = item.Member.Id, Username = item.Member.Username, Email = item.Member.Email},

                    // }).ToList()
                };

                eventDTOs.Add(eventDTO);
            }

            return Ok(eventDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEventDTO>> GetEvent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ev = await _context.Events
                .Include(ev => ev.Organizer)
                .Include(ev => ev.Enrollments)
                    .ThenInclude(e => e.Member)
                .AsNoTracking()
                .FirstOrDefaultAsync(ev => ev.Id == id);
            
            if (ev == null)
            {
                return NotFound();
            }            

            GetEventDTO eventDTO = new GetEventDTO
            {
                Id = ev.Id,
                Name = ev.Name,
                Address1 = ev.Address1,
                Address2 = ev.Address2,
                City = ev.City,
                State = ev.State,
                Zip = ev.Zip,
                Description = ev.Description,
                ImageURL = ev.ImageURL,
                Organizer = new MemberDTO{Id = ev.Organizer.Id, Username = ev.Organizer.Username, Email = ev.Organizer.Email},
                Enrollments = ev.Enrollments.Select(item => new EventEnrollmentDTO
                {
                    Id = item.Id,
                    Member = new MemberDTO{Id = item.Member.Id, Username = item.Member.Username, Email = item.Member.Email},
                }).ToList()
            };

            return Ok(eventDTO);

        }

        [HttpPost]
        public async Task<ActionResult<GetEventDTO>> AddEvent(CreateEventDTO newEvent)
        {
            if (newEvent == null) {
                return BadRequest();
            }
            // does organizer exist in db?
            var organizer = await _context.Members.FirstOrDefaultAsync(m => m.Email == newEvent.OrganizerEmail);

            // if organizer is not in DB then create new member
            if (organizer == null) {
                organizer = new Member {
                    Email = newEvent.OrganizerEmail
                };
            }

            // convert newEvent to Event
            var ev = new Event
            {
                Name = newEvent.Name,
                Address1 = newEvent.Address1,
                Address2 = newEvent.Address2,
                City = newEvent.City,
                State = newEvent.State,
                Zip = newEvent.Zip,
                Description = newEvent.Description,
                ImageURL = newEvent.ImageURL,
                Organizer = organizer,
            };            

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            var enrollment = new Enrollment
            {
                MemberId = ev.Organizer.Id,
                EventId = ev.Id
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            var eventDTO = new GetEventDTO
            {
                Id = ev.Id,
                Name = ev.Name,
                Address1 = ev.Address1,
                Address2 = ev.Address2,
                City = ev.City,
                State = ev.State,
                Zip = ev.Zip,
                Description = ev.Description,
                ImageURL = ev.ImageURL,
                Organizer = new MemberDTO
                {
                    Id = organizer.Id,
                    Email = ev.Organizer.Email
                }
            };

            return Ok(eventDTO);
        }
    }
}