using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class ReportCommand : CommandBase, IReportCommand
    {
        public string Execute()
        {
            return Log(nameof(ReportCommand), Coordinates.X + "," + Coordinates.Y + "," + Coordinates.F);
        }
    }
}
