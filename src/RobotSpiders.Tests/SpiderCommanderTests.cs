using Moq;
using NUnit.Framework;
using RobotSpiders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace RobotSpiders.Tests
{
    public class SpiderCommanderTests
    {
        private Mock<ISpider> _spider;
        private Mock<IAmATwoDimensionalSurface> _wall;
        private Position _startPosition;
        private Orientation _startOrientation;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _spider = new Mock<ISpider>();
            _wall = new Mock<IAmATwoDimensionalSurface>();
            _startPosition = new Position { X = 0, Y = 0 };
            _startOrientation = Orientation.Up;
        }

        [Test]
        public void Commander_InterpretsCommandsCorrectly()
        {
            var commander = SpiderCommander.With().Wall(_wall.Object.Width, _wall.Object.Height).Spider(_spider.Object);
            commander.AddCommand('L');
            commander.AddCommand('R');
            commander.AddCommand('F');
            commander.Commands.Count.Should().Be(3);
            commander.Commands.ElementAt<SpiderCommand>(0).Should().BeOfType<RotateLeftCommand>();
            commander.Commands.ElementAt<SpiderCommand>(1).Should().BeOfType<RotateRightCommand>();
            commander.Commands.ElementAt<SpiderCommand>(2).Should().BeOfType<MoveForwardCommand>();
        }

        [Test]
        [TestCase('h')]
        [TestCase('Z')]
        [TestCase('X')]
        [TestCase('b')]
        public void Commander_DoesNotAddAnIncorrectCommand(char command)
        {
            var commander = SpiderCommander.With().Wall(_wall.Object.Width, _wall.Object.Height).Spider(_spider.Object);
            commander.AddCommand(command);
            commander.Commands.Count.Should().Be(0);
        }

        [Test]
        [TestCase("RFFFLFF")]
        public void Commander_CanAddMultipleCommandsInOneGo(string commands)
        {
            var commander = SpiderCommander.With().Wall(_wall.Object.Width, _wall.Object.Height).Spider(_spider.Object);
            commander.AddCommand(commands);
            commander.Commands.Count.Should().Be(7);
            commander.Commands.ElementAt<SpiderCommand>(0).Should().BeOfType<RotateRightCommand>();
            commander.Commands.ElementAt<SpiderCommand>(1).Should().BeOfType<MoveForwardCommand>();
            commander.Commands.ElementAt<SpiderCommand>(2).Should().BeOfType<MoveForwardCommand>();
            commander.Commands.ElementAt<SpiderCommand>(3).Should().BeOfType<MoveForwardCommand>();
            commander.Commands.ElementAt<SpiderCommand>(4).Should().BeOfType<RotateLeftCommand>();
            commander.Commands.ElementAt<SpiderCommand>(5).Should().BeOfType<MoveForwardCommand>();
            commander.Commands.ElementAt<SpiderCommand>(6).Should().BeOfType<MoveForwardCommand>();
        }

        [Test]
        public void Commander_ExecutesTheCommandsCorrectly()
        {
            var executedSpiderCommands = new List<string>();
            _spider.Setup(s => s.MoveForward()).Callback(() => executedSpiderCommands.Add("F"));
            _spider.Setup(s => s.RotateLeft()).Callback(() => executedSpiderCommands.Add("L"));
            _spider.Setup(s => s.RotateRight()).Callback(() => executedSpiderCommands.Add("R"));

            var commander = SpiderCommander.With().Wall(_wall.Object.Width, _wall.Object.Height).Spider(_spider.Object);

            commander.AddCommand('F');
            commander.AddCommand('F');
            commander.AddCommand('R');
            commander.AddCommand('F');
            commander.AddCommand('F');
            commander.AddCommand('L');
            commander.Execute();

            executedSpiderCommands.Count().Should().Be(6);
            executedSpiderCommands.ElementAt<string>(0).Should().Be("F");
            executedSpiderCommands.ElementAt<string>(1).Should().Be("F");
            executedSpiderCommands.ElementAt<string>(2).Should().Be("R");
            executedSpiderCommands.ElementAt<string>(3).Should().Be("F");
            executedSpiderCommands.ElementAt<string>(4).Should().Be("F");
            executedSpiderCommands.ElementAt<string>(5).Should().Be("L");
        }

        [Test]
        [TestCase(7, 15, 2, 4, Orientation.Left, "FLFLFRFFLF")]
        public void Commander_WhenFullySetUpWithAWallAndASpider_CommandsTheSpiderCorrectly(int wallWidth, int wallHeight, int startPositionX, int startPositionY, Orientation startOrientation, string commands)
        {
            _wall.Setup(w => w.Width).Returns(wallWidth);
            _wall.Setup(w => w.Height).Returns(wallHeight);
            var spider = Spider.WithStartOrientationAndPosition(startOrientation, new Position { X = startPositionX, Y = startPositionY });

            var commander = SpiderCommander.With().Wall(_wall.Object.Width, _wall.Object.Height).Spider(spider);
            commander.AddCommand(commands);
            commander.Execute();

            spider.Orientation.Should().Be(Orientation.Right);
            spider.Position.X.Should().Be(3);
            spider.Position.Y.Should().Be(1);
        }
    }
}
