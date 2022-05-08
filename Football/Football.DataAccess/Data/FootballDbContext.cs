using Football.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.DataAccess.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options)
        {

        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasOne(p => p.Club).WithMany(c => c.Players).HasForeignKey(p => p.ClubId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Club>().HasOne(c => c.League).WithMany(l => l.Clubs).HasForeignKey(c => c.LeagueId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Club>().HasOne(c => c.Coach).WithMany().HasForeignKey(c => c.CoachId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
