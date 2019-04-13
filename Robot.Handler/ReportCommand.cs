using Robot.Contract;
using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class ReportCommand : CommandBase, IReportCommand
    {
        private readonly IRepository<Coordinate> coordinateRepository;

        public ReportCommand(IRepository<Coordinate> coordinateRepository)
        {
            this.coordinateRepository = coordinateRepository;
        }

        public string Execute(Coordinate coordinate, PositionSetting setting)
        {
            return Log(coordinate.X + "," + coordinate.Y + "," + coordinate.F);
        }

        protected override string Log(string message)
        {
            return message;
        }
    }
}
