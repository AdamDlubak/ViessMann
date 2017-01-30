using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PolTrain.Models;

namespace PolTrain.DAL
{
    public class PolTrainContext : IdentityDbContext<ApplicationUser>
    {
        public PolTrainContext() : base("PolTrainContext")
        {
            
        }

        static PolTrainContext()
        {
            Database.SetInitializer<PolTrainContext>(new PolTrainInitializer());
        }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Concession> Concessions { get; set; }

        public static PolTrainContext Create()
        {
            return new PolTrainContext();
        }
    }
}