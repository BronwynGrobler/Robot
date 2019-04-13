using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class CommandBase
    {
        protected Coordinate Coordinates { get; set; }
        protected PositionSetting Setting { get; set; }

        protected string Log(string command)
        {
            return string.Format("{0} executed", command);
        }

        protected string Log(string command, string message)
        {
            return string.Format("{0} executed. {1}", command, message);
        }
    }
}
