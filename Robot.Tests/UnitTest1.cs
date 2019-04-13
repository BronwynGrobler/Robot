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
        public void Test1_SuccessAsync()
        {
            //PLACE 0,0,NORTH
            //MOVE
            //MOVE
            //RIGHT
            //REPORT
            //Output: 0,2,EAST

            // Arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();
            
            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // Act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);
            main.Place(0, 0, EDirection.NORTH);
            main.Execute(new MoveCommand(mockCoordinateRepo));
            main.Execute(new MoveCommand(mockCoordinateRepo));
            main.Execute(new RightCommand(mockCoordinateRepo));
            var result = main.Execute(new ReportCommand(mockCoordinateRepo));

            // Assert
            Assert.Equal("0,2,EAST", result.Result);
        }

        [Fact]
        public async Task Test2_SuccessAsync()
        {
            //PLACE 0,0,NORTH
            //LEFT
            //REPORT
            //Output: 0,0,WEST

            // Arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // Act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);
            await main.Place(0, 0, EDirection.NORTH);
            await main.Execute(new LeftCommand(mockCoordinateRepo));
            var result = await main.Execute(new ReportCommand(mockCoordinateRepo));

            // Assert
            Assert.Equal("0,0,WEST", result);
        }

        [Fact]
        public async Task Test3_SuccessAsync()
        {
            //PLACE 1,2,EAST
            //MOVE
            //MOVE
            //MOVE
            //LEFT
            //MOVE
            //REPORT
            //Output: 3,3,NORTH

            // Arrange
            var mockCoordinateRepo = new DummyDb<Coordinate>();
            var mockPlaceCommand = new PlaceCommand(mockCoordinateRepo);
            var mockPositionRepo = new Mock<IPositionSettingRepository>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            // Act
            var main = new CommandService(mockCoordinateRepo, mockPositionRepo.Object, mockPlaceCommand);
            await main.Place(1, 2, EDirection.EAST);
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            await main.Execute(new LeftCommand(mockCoordinateRepo));
            await main.Execute(new MoveCommand(mockCoordinateRepo));
            var result = await main.Execute(new ReportCommand(mockCoordinateRepo));

            // Assert
            Assert.Equal("4,3,NORTH", result);
        }
    }

    // Example of mocking up your own repository for testing
    public class DummyDb<T> : IRepository<T>
        where T : Entity
    {

        private IList<T> holder = new List<T>();

        public void Add(T entity)
        {
            holder.Add(entity);
        }

        public T Last()
        {
            return holder.ToList().OrderBy(h => h.Id).LastOrDefault();
        }
    }
}
