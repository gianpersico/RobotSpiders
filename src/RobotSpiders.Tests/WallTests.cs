using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RobotSpiders.Domain;

namespace RobotSpiders.Tests
{
    public class WallTests
    {
        [Test]
        public void Wall_SetsWidthAndHeightCorrectly()
        {
            var wall = new Wall(7,15);
            wall.Width.Should().Be(7);
            wall.Height.Should().Be(15);
        }

        [Test]
        public void Wall_HasADefaultOccupiedPosition_InTheBottomLeftHandCorner()
        {
            var wall = new Wall(5, 5);
            wall.OccupiedPosition.X.Should().Be(0);
            wall.OccupiedPosition.Y.Should().Be(0);
        }
    }
}
