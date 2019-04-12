using Robot.Handler;
using Robot.Model;
using System;

namespace Robot.Service
{
    public class CommandService : ICommandService
    {
        public string Execute(ICommand command)
        {
            return command.Execute();
        }
    }
}
