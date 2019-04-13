
using Robot.Handler;
using Robot.Model;
using System.Threading.Tasks;

namespace Robot.Service
{
    public interface ICommandService
    {
        Task<string> Execute(ICommand command);

        Task<string> Place(int X, int Y, EDirection F);
    }
}
