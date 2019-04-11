using Microsoft.Extensions.Logging;
using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public class RobotAction : IRobotAction
    {
        private readonly ILogger<RobotAction> logger;
        private readonly IRepository<Coordinate> coordinateRepository;
        private readonly IRepository<ReportLog> reportRepository;
        private readonly IPositionSettingRepository settingRepository;
        private readonly IValidator validator;
        private Coordinate Coordinates { get; set; }
        private PositionSetting Settings { get; set; }


        public RobotAction(ILogger<RobotAction> logger, IRepository<Coordinate> coordinateRepository, IRepository<ReportLog> reportRepository, IPositionSettingRepository settingRepository, IValidator validator)
        {
            this.logger = logger;
            this.coordinateRepository = coordinateRepository;
            this.reportRepository = reportRepository;
            this.settingRepository = settingRepository;
            this.validator = validator;
        }

        public void Execute(string[] param)
        {
            Coordinates = this.coordinateRepository.Last();
            var cmd = Enum.Parse<ECommand>(param[0].ToUpper());

            if (cmd != ECommand.PLACE && Coordinates == null)
            {
                logger.LogError("Please make a placement first.");
                return;
            }

            switch (cmd)
            {
                case ECommand.REPORT:
                    this.Report();
                    break;

                case ECommand.PLACE:
                    var clean = this.validator.IsValid(param);

                    if (clean == null)
                    {
                        return;
                    }

                    this.Place(int.Parse(clean[1]), int.Parse(clean[2]), Enum.Parse<EDirection>(clean[3].ToUpper()));
                    break;

                case ECommand.MOVE:
                case ECommand.LEFT:
                case ECommand.RIGHT:
                    Settings = this.settingRepository.Setting(Coordinates.F);
                    ExecuteAction(cmd);
                    break;

                default:
                    logger.LogError("Command was not found " + cmd);
                    break;
            }
        }

        private void ExecuteAction(ECommand cmd)
        {
            switch (cmd)
            {
                case ECommand.MOVE:
                    this.Move();
                    break;

                case ECommand.LEFT:
                    this.Left();
                    break;

                case ECommand.RIGHT:
                    this.Right();
                    break;

                default:
                    logger.LogError("Command was not found " + cmd);
                    break;
            }
        }

        private void Left()
        {
            this.coordinateRepository.Add(new Coordinate() { X = Coordinates.X, Y = Coordinates.Y, F = Settings.Left });
        }

        private void Right()
        {
            this.coordinateRepository.Add(new Coordinate() { X = Coordinates.X, Y = Coordinates.Y, F = Settings.Right });
        }

        private void Move()
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

            if (X > 5 || X < 0 || Y > 5 || Y < 0)
            {
                logger.LogError("Robot will fall off the table.");
                return;
            }

            this.coordinateRepository.Add(new Coordinate() { X = X, Y = Y, F = Coordinates.F });
        }

        private void Place(int X, int Y, EDirection F)
        {
            if (X > 5 || X < 0 || Y > 5 || Y < 0)
            {
                logger.LogError("This is not a valid placement - robot will fall off the table.");
                return;
            }

            this.coordinateRepository.Add(new Coordinate() { X = X, Y = Y, F = F });
        }

        private void Report()
        {
            var result = Coordinates.X + "," + Coordinates.Y + "," + Coordinates.F;

            // This is only used for testing purposes
            this.reportRepository.Add(new ReportLog() { Message = result.Trim() });
            logger.LogWarning(result);
        }
    }
}
