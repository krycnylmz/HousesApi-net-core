using System;
using HousesApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<House> Houses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}
