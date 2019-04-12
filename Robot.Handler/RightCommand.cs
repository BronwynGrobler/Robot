using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class RightCommand : CommandBase, IRightCommand
    {
        private readonly IRepository<Coordinate> coordinateRepository;

        public RightCommand(IRepository<Coordinate> coordinateRepository)
        {
            this.coordinateRepository = coordinateRepository;
        }

        public string Execute()
        {
            this.coordinateRepository.Add(new Coordinate() { X = Coordinates.X, Y = Coordinates.Y, F = Settings.Right });
            return Log(nameof(RightCommand));
        }
    }
}
