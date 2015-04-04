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
    public class SpiderCommandTests
    {
        [Test]
        public void RotateLeftCommand_CallsRotateLeft_OnTheSpider()
        {
            var spider = new Mock<ISpider>();
            int rotateCalls = 0;
            spider.Setup(s => s.RotateLeft()).Callback(() => rotateCalls++);
            var cmd = new RotateLeftCommand(spider.Object);
            cmd.Execute();
            rotateCalls.Should().Be(1);
        }

        [Test]
        public void RotateRightCommand_CallsRotateRight_OnTheSpider()
        {
            var spider = new Mock<ISpider>();
            int rotateCalls = 0;
            spider.Setup(s => s.RotateRight()).Callback(() => rotateCalls++);
            var cmd = new RotateRightCommand(spider.Object);
            cmd.Execute();
            rotateCalls.Should().Be(1);
        }

        [Test]
        public void MoveForwardCommand_CallsMoveForward_OnTheSpider()
        {
            var spider = new Mock<ISpider>();
            int forwardCalls = 0;
            spider.Setup(s => s.MoveForward()).Callback(() => forwardCalls++);
            var cmd = new MoveForwardCommand(spider.Object);
            cmd.Execute();
            forwardCalls.Should().Be(1);
        }
    }
}
