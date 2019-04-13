using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Handler
{
    public interface IPlaceCommand 
    {
        string Execute(int X, int Y, EDirection F);
    }
}
