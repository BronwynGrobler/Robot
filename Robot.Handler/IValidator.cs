using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public interface IValidator
    {
        string[] IsValid(string[] param);
    }
}
