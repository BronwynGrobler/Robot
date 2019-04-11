using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public interface IRobotAction
    {
        void Execute(string[] param);
    }
}
