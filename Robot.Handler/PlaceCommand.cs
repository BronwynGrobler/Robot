using Robot.Contract;
using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Handler
{
    public class PlaceCommand : CommandBase, IPlaceCommand
    {
        private readonly IRepository<Coordinate> coordinateRepository;

        public PlaceCommand(IRepository<Coordinate> coordinateRepository)
        {
            this.coordinateRepository = coordinateRepository;
        }

        public async Task<string> Execute(int X, int Y, EDirection F)
        {
            if (Helper.Verify(X, Y))
            {
                return Log(nameof(PlaceCommand), "This is not a valid placement - robot will fall off the table.");
            }

            await this.coordinateRepository.Add(new Coordinate() { X = X, Y = Y, F = F });
            return Log(nameof(PlaceCommand));
        }
    }
}
