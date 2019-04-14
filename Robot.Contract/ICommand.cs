using Robot.Model;
using System;
using System.Threading.Tasks;

namespace Robot.Contract
{
    public interface ICommand
    {
        Task<String> Execute(Coordinate coordinate, PositionSetting setting);
    }
}
