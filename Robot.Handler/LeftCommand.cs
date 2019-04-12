using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class LeftCommand : CommandBase, ILeftCommand
    {
        private readonly IRepository<Coordinate> coordinateRepository;

        public LeftCommand(IRepository<Coordinate> coordinateRepository)
        {
            this.coordinateRepository = coordinateRepository;
        }

        public string Execute()
        {
            this.coordinateRepository.Add(new Coordinate() { X = Coordinates.X, Y = Coordinates.Y, F = Settings.Left });
            return Log(nameof(LeftCommand));
        }
    }
}
