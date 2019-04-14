using Microsoft.Extensions.Logging;
using Moq;
using Robot.Contract;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using Robot.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Robot.Tests
{
    public class ActionTest
    {
        [Fact]
        public async Task Test1_success()
        {
            //place 0,0,north
            //move
            //move
            //right
            //report
            //output: 0,2,east

            // arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).ReturnsAsync(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).ReturnsAsync(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).ReturnsAsync(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).ReturnsAsync(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);

            await main.Place(0, 0, EDirection.NORTH);
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new RightCommand(mockCoordinateRepo));
            var result = main.Execute(new ReportCommand(mockCoordinateRepo));

            // assert
            Assert.Equal("0,2,EAST", result.Result);
        }

        [Fact]
        public async Task Test2_success()
        {
            //place 0,0,north
            //left
            //report
            //output: 0,0,west

            // arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).ReturnsAsync(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).ReturnsAsync(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).ReturnsAsync(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).ReturnsAsync(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);

            await main.Place(0, 0, EDirection.NORTH);
            await main.Execute(new LeftCommand(mockCoordinateRepo));
            var result = main.Execute(new ReportCommand(mockCoordinateRepo));

            // assert
            Assert.Equal("0,0,WEST", result.Result);
        }

        [Fact]
        public async Task Test3_success()
        {
            //PLACE 1,2,EAST
            //MOVE
            //MOVE
            //MOVE
            //LEFT
            //MOVE
            //REPORT
            //Output: 4,3,NORTH

            // arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).ReturnsAsync(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).ReturnsAsync(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).ReturnsAsync(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).ReturnsAsync(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);

            await main.Place(1, 2, EDirection.EAST);
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new LeftCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            var result = main.Execute(new ReportCommand(mockCoordinateRepo));

            // assert
            Assert.Equal("4,3,NORTH", result.Result);
        }


        // Example of mocking up your own repository for testing
        public class DummyDb<T> : IRepository<T>
            where T : Entity
        {

            private IList<T> holder = new List<T>();

            public async Task Add(T entity)
            {
                await Task.Run(() =>
                {
                    holder.Add(entity);
                });                
            }

            public async Task<T> Last()
            {
                return await Task.Run(() =>
                {
                    return holder.ToList().OrderBy(h => h.Id).LastOrDefault();
                });
            }
        }
    }
}
