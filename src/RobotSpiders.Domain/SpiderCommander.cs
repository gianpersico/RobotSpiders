using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpiders.Domain
{
    public class SpiderCommander
    {
        private ISpider _spider;
        private IAmATwoDimensionalSurface _wall;
        public Queue<SpiderCommand> Commands { get; private set; }

        private SpiderCommander() { }

        public static SpiderCommander With()
        {
            return new SpiderCommander();
        }

        public SpiderCommander Wall(int width, int height)
        {
            _wall = new Wall(width, height);
            return this;
        }

        public SpiderCommander Spider(ISpider spider)
        {
            _spider = spider;
            return this;
        }

        public void AddCommand(char command)
        {
            if (Commands == null) Commands = new Queue<SpiderCommand>();
            SpiderCommand commandToEnqueue = null;

            switch (command.ToString().ToLower())
            {
                case "l":
                    commandToEnqueue = new RotateLeftCommand(_spider);
                    break;
                case "r":
                    commandToEnqueue = new RotateRightCommand(_spider);
                    break;
                case "f":
                    commandToEnqueue = new MoveForwardCommand(_spider);
                    break;
            }
            if (commandToEnqueue != null) Commands.Enqueue(commandToEnqueue);
        }

        public void AddCommand(string commands)
        {
            foreach (var command in commands)
            {
                AddCommand(command);
            }
        }

        public void Execute()
        {
            while (Commands.Count > 0)
            {
                var nextCommand = Commands.Dequeue();
                nextCommand.Execute();
            }
        }
    }
}
