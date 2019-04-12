
using Robot.Handler;

namespace Robot.Service
{
    public interface ICommandService
    {
        string Execute(ICommand command);
    }
}
