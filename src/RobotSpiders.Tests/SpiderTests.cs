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
    public class SpiderTests
    {
        [Test]
        public void DefaultSpider_IsOrientatedUp_AndAtPositionZeroZero()
        {
            var spider = Spider.CreateDefault();
            spider.Orientation.Should().Be(Orientation.Up);
            spider.Position.X.Should().Be(0);
            spider.Position.Y.Should().Be(0);
        }

        [Test]
        [TestCase(Orientation.Up, Orientation.Right)]
        [TestCase(Orientation.Right, Orientation.Down)]
        [TestCase(Orientation.Down, Orientation.Left)]
        [TestCase(Orientation.Left, Orientation.Up)]
        public void Spider_WhenAskedToRotateRight_ShouldBeOrientedCorrectly_DependentOnItsStartOrientation(Orientation startOrientation, Orientation expectedOrientation)
        {
            var spider = Spider.WithStartOrientationAndPosition(startOrientation, new Position { X = 0, Y = 0 });
            spider.RotateRight();
            spider.Orientation.Should().Be(expectedOrientation);
        }
        
        [Test]
        [TestCase(Orientation.Up, Orientation.Left)]
        [TestCase(Orientation.Right, Orientation.Up)]
        [TestCase(Orientation.Down, Orientation.Right)]
        [TestCase(Orientation.Left, Orientation.Down)]
        public void Spider_WhenAskedToRotateLeft_ShouldBeOrientedCorrectly_DependentOnItsStartOrientation(Orientation startOrientation, Orientation expectedOrientation)
        {
            var spider = Spider.WithStartOrientationAndPosition(startOrientation, new Position { X = 0, Y = 0 });
            spider.RotateLeft();
            spider.Orientation.Should().Be(expectedOrientation);
        }

        [Test]
        [TestCase(Orientation.Up, 0, 0, 0, 1)]
        [TestCase(Orientation.Right, 0, 0, 1, 0)]
        [TestCase(Orientation.Left, 3, 3, 2, 3)]
        [TestCase(Orientation.Down, 3, 3, 3, 2)]
        public void Spider_WhenAskedToMoveForward_ShouldMoveToTheCorrectPosition_DependentOnOrientationAndStartPosition(Orientation startOrientation, int startX, int startY, int expectedX, int expectedY)
        {
            var spider = Spider.WithStartOrientationAndPosition(startOrientation, new Position { X = startX, Y = startY });
            spider.MoveForward();
            spider.Position.X.Should().Be(expectedX);
            spider.Position.Y.Should().Be(expectedY);
        }
    }
}
