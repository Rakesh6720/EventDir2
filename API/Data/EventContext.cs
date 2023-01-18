using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EventContext : IdentityDbContext<User>
    {
        public EventContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events {get; set;}
        public DbSet<Member> Members {get; set;}     

        public DbSet<Enrollment> Enrollments {get; set;}  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Enrollment>()
                .HasKey(en => new { en.EventId, en.MemberId});
            builder.Entity<Enrollment>()
                .HasOne(en => en.Event)
                .WithMany(en => en.Enrollments)
                .HasForeignKey(en => en.EventId);
            builder.Entity<Enrollment>()
                .HasOne(en => en.Member)
                .WithMany(en => en.Enrollments)
                .HasForeignKey(en => en.MemberId);

            builder.Entity<IdentityRole>()                
                .HasData(
                    new IdentityRole{Name = "Member", NormalizedName = "MEMBER"},
                    new IdentityRole{Name = "Admin", NormalizedName = "ADMIN"}
                );
            
        }
    }
}