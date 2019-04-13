using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public abstract class CommandBase
    {
        protected virtual string Log(string command)
        {
            return string.Format("{0} executed", command);
        }

        protected virtual string Log(string command, string message)
        {
            return string.Format("{0} executed. {1}", command, message);
        }
    }
}
