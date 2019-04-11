using Microsoft.Extensions.Logging;
using Moq;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Robot.Tests
{
    public class ActionTest
    {
        [Fact]
        public void Test1_Success()
        {
            //PLACE 0,0,NORTH
            //MOVE
            //MOVE
            //RIGHT
            //REPORT
            //Output: 0,2,WEST

            // Arrange
            var mockLogging = new Mock<ILogger<RobotAction>>();
            var mockCoordinateRepo = new Mock<DummyDb<Coordinate>>();
            var mockReportRepo = new Mock<DummyDb<ReportLog>>();
            var mockPositionRepo = new Mock<IPositionSettingRepository>();
            var mockValidator = new Mock<IValidator>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            mockValidator.Setup(x => x.IsValid(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() })).Returns(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() });

            // Act
            var main = new RobotAction(mockLogging.Object, mockCoordinateRepo.Object, mockReportRepo.Object, mockPositionRepo.Object, mockValidator.Object);
            main.Execute(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.RIGHT.ToString() });
            main.Execute(new string[] { ECommand.REPORT.ToString() });

            // Assert
            Assert.Equal("0,2,EAST", mockReportRepo.Object.Last().Message);

        }

        [Fact]
        public void Test2_Success()
        {
            //PLACE 0,0,NORTH
            //LEFT
            //REPORT
            //Output: 0,0,WEST

            // Arrange
            var mockLogging = new Mock<ILogger<RobotAction>>();
            var mockCoordinateRepo = new Mock<DummyDb<Coordinate>>();
            var mockReportRepo = new Mock<DummyDb<ReportLog>>();
            var mockPositionRepo = new Mock<IPositionSettingRepository>();
            var mockValidator = new Mock<IValidator>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            mockValidator.Setup(x => x.IsValid(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() })).Returns(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() });

            // Act
            var main = new RobotAction(mockLogging.Object, mockCoordinateRepo.Object, mockReportRepo.Object, mockPositionRepo.Object, mockValidator.Object);
            main.Execute(new string[] { ECommand.PLACE.ToString(), "0", "0", EDirection.NORTH.ToString() });
            main.Execute(new string[] { ECommand.LEFT.ToString() });
            main.Execute(new string[] { ECommand.REPORT.ToString() });

            // Assert
            Assert.Equal("0,0,WEST", mockReportRepo.Object.Last().Message);
        }

        [Fact]
        public void Test3_Success()
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
            var mockLogging = new Mock<ILogger<RobotAction>>();
            var mockCoordinateRepo = new Mock<DummyDb<Coordinate>>();
            var mockReportRepo = new Mock<DummyDb<ReportLog>>();
            var mockPositionRepo = new Mock<IPositionSettingRepository>();
            var mockValidator = new Mock<IValidator>();

            mockPositionRepo.Setup(x => x.Setting(EDirection.NORTH)).Returns(new PositionSetting() { Id = 1, Facing = EDirection.NORTH, Left = EDirection.WEST, Right = EDirection.EAST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.SOUTH)).Returns(new PositionSetting() { Id = 2, Facing = EDirection.SOUTH, Left = EDirection.EAST, Right = EDirection.WEST, Coordinate = ECoordinate.Y });
            mockPositionRepo.Setup(x => x.Setting(EDirection.EAST)).Returns(new PositionSetting() { Id = 3, Facing = EDirection.EAST, Left = EDirection.NORTH, Right = EDirection.SOUTH, Coordinate = ECoordinate.X });
            mockPositionRepo.Setup(x => x.Setting(EDirection.WEST)).Returns(new PositionSetting() { Id = 4, Facing = EDirection.WEST, Left = EDirection.SOUTH, Right = EDirection.NORTH, Coordinate = ECoordinate.X });

            mockValidator.Setup(x => x.IsValid(new string[] { ECommand.PLACE.ToString(), "1", "2", EDirection.EAST.ToString() })).Returns(new string[] { ECommand.PLACE.ToString(), "1", "2", EDirection.EAST.ToString() });

            // Act
            var main = new RobotAction(mockLogging.Object, mockCoordinateRepo.Object, mockReportRepo.Object, mockPositionRepo.Object, mockValidator.Object);
            main.Execute(new string[] { ECommand.PLACE.ToString(), "1", "2", EDirection.EAST.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.LEFT.ToString() });
            main.Execute(new string[] { ECommand.MOVE.ToString() });
            main.Execute(new string[] { ECommand.REPORT.ToString() });

            // Assert
            Assert.Equal("4,3,NORTH", mockReportRepo.Object.Last().Message);
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
