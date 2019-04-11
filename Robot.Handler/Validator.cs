using Microsoft.Extensions.Logging;
using Robot.Model;
using System;
using System.Linq;

namespace Robot.Handler
{
    public class Validator : IValidator
    {
        private readonly ILogger<Validator> logger;

        public Validator(ILogger<Validator> logger)
        {
            this.logger = logger;
        }

        public string[] IsValid(string[] param)
        {
            var clean = param.Where(p => p.Length >= 1).ToArray();

            if (clean.Count() != 4)
            {
                this.logger.LogError("Incorrect parameters were entered.");
                return null;
            }

            var resultX = int.TryParse(clean[1], out int x);
            if (!resultX)
            {
                this.logger.LogError("Coordinate X is not an integer.");
                return null;
            }

            var resultY = int.TryParse(clean[2], out int y);
            if (!resultY)
            {
                this.logger.LogError("Coordinate Y is not an integer.");
                return null;
            }

            var result = Enum.TryParse<EDirection>(clean[3].ToUpper(), out EDirection facing);
            if (!result)
            {
                this.logger.LogError("A direction was not entered.");
                return null;
            }

            return clean;
        }
    }
}
