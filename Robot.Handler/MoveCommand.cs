using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class MoveCommand : CommandBase, IMoveCommand
    {
        private readonly IRepository<Coordinate> coordinateRepository;

        public MoveCommand(IRepository<Coordinate> coordinateRepository)
        {
            this.coordinateRepository = coordinateRepository;
        }

        public string Execute()
        {
            var X = Coordinates.X;
            var Y = Coordinates.Y;

            switch (Settings.Coordinate)
            {
                case ECoordinate.X:
                    X = X + 1;
                    break;

                case ECoordinate.Y:
                    Y = Y + 1;
                    break;
            }

            if (Helper.Verify(X, Y))
            {
                return Log(nameof(MoveCommand), "Robot will fall off the table!");
            }

            this.coordinateRepository.Add(new Coordinate() { X = X, Y = Y, F = Coordinates.F });
            return Log(nameof(MoveCommand));
        }
    }
}
