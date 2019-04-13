using Robot.Contract;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using System;
using System.Threading.Tasks;

namespace Robot.Service
{
    public class CommandService : ICommandService
    {

        private readonly IRepository<Coordinate> coordinateRepository;
        private readonly IPositionSettingRepository settingRepository;
        private readonly IPlaceCommand placeCommand;

        public CommandService(IRepository<Coordinate> coordinateRepository, IPositionSettingRepository settingRepository, IPlaceCommand placeCommand)
        {
            this.coordinateRepository = coordinateRepository;
            this.settingRepository = settingRepository;
            this.placeCommand = placeCommand;
        }

        public async Task<string> Execute(ICommand command)
        {
            var coordinates = this.coordinateRepository.Last();

            if (coordinates == null)
            {
                return ("Please make a placement first.");
            }

            return await Task.Run(() =>
            {
                return command.Execute(coordinates, this.settingRepository.Setting(coordinates.F));
            });
        }

        public async Task<string> Place(int X, int Y, EDirection F)
        {
            return await Task.Run(() =>
            {
                return this.placeCommand.Execute(X, Y, F);
            });
        }
    }
}
