using Microsoft.EntityFrameworkCore;
using Robot.Model;
using System;

namespace Robot.Repository
{
    public class RobotDbContext : DbContext
    {
        public RobotDbContext(DbContextOptions<RobotDbContext> options)
       : base(options)
        {
        }

        public DbSet<Coordinate> Coordinates { get; set; }

        public DbSet<PositionSetting> Directions { get; set; }

        public DbSet<ReportLog> ReportLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup right and left properties
            // Setup the coordinate that will increment when facing a certain direction
            modelBuilder.Entity<PositionSetting>().HasData(
                new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y },
                new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y },
                new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X },
                new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X }
                );
        }
    }
}
