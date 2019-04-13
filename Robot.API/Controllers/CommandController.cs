using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Robot.Handler;
using Robot.Model;
using Robot.Service;

namespace Robot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : Controller
    {
        private readonly ICommandService commandService;
        private readonly ILeftCommand leftCommand;
        private readonly IRightCommand rightCommand;
        private readonly IMoveCommand moveCommand;

        public CommandController(ICommandService commandService, ILeftCommand leftCommand, IRightCommand rightCommand, IMoveCommand moveCommand)
        {
            this.commandService = commandService;
            this.rightCommand = rightCommand;
            this.leftCommand = leftCommand;
            this.moveCommand = moveCommand;
        }

        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("Left")]
        public async Task<string> Left()
        {
            return await this.commandService.Execute(leftCommand);
        }

        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("Right")]
        public async Task<string> Right()
        {
            return await this.commandService.Execute(rightCommand);
        }

        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("Move")]
        public async Task<string> Move()
        {
            return await this.commandService.Execute(moveCommand);
        }

        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("Place")]
        public async Task<string> Place(int X, int Y, EDirection F)
        {
            return await this.commandService.Place(X, Y, F);
        }
    }
}
