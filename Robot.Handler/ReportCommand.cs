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
            return Log(nameof(ReportCommand), coordinate.X + "," + coordinate.Y + "," + coordinate.F);
        }
    }
}
