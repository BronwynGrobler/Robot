using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Contract
{
    public interface IPlaceCommand 
    {
        string Execute(int X, int Y, EDirection F);
    };
}
