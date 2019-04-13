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

        public string Execute(Coordinate coordinate, PositionSetting setting)
        {
            this.coordinateRepository.Add(new Coordinate() { X = coordinate.X, Y = coordinate.Y, F = setting.Left });
            return Log(nameof(LeftCommand));
        }
    }
}
