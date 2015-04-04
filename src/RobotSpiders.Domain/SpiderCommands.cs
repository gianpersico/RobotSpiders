using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpiders.Domain
{
    public abstract class SpiderCommand
    {
        protected ISpider _spider;
        public SpiderCommand(ISpider spider)
        {
            _spider = spider;
        }
        public abstract void Execute();
    }

    public class RotateLeftCommand : SpiderCommand
    {
        public RotateLeftCommand(ISpider spider) : base(spider) { }

        public override void Execute()
        {
            _spider.RotateLeft();
        }
    }

    public class RotateRightCommand : SpiderCommand
    {
        public RotateRightCommand(ISpider spider) : base(spider) { }

        public override void Execute()
        {
            _spider.RotateRight();
        }
    }

    public class MoveForwardCommand : SpiderCommand
    {
        public MoveForwardCommand(ISpider spider) : base(spider) { }

        public override void Execute()
        {
            _spider.MoveForward();
        }
    }
}
