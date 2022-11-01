using Data.Guest;
using Data.Room;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options) { }

    public DbSet<Domain.Entities.Guest> Guests { get; set; }
    public DbSet<Domain.Entities.Room> Rooms { get; set; }
    public DbSet<Domain.Entities.Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}