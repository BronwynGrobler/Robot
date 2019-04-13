using Robot.Model;
using System;

namespace Robot.Contract
{
    public interface ICommand
    {
        string Execute(Coordinate coordinate, PositionSetting setting);
    }
}
