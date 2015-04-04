using RobotSpiders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpiders.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteOutput("Please enter the width and height values for the wall e.g. 7 15");
            var coordinates = ReadInput();
            var coordinateParts=coordinates.Split(' ');
            var x = int.Parse(coordinateParts[0]);
            var y = int.Parse(coordinateParts[1]);

            WriteOutput("Please enter the initial position and orientation for the spider e.g. 2 4 Left");
            var positionAndOrientation = ReadInput();
            var positionAndOrientationParts = positionAndOrientation.Split(' ');
            Orientation orientation;
            switch (positionAndOrientationParts[2].ToLower())
            {
                case "left":
                    orientation = Orientation.Left;
                    break;
                case "right":
                    orientation = Orientation.Right;
                    break;
                case "down":
                    orientation = Orientation.Down;
                    break;
                default:
                    orientation = Orientation.Up;
                    break;

            }
            var spider = Spider.WithStartOrientationAndPosition(orientation, new Position { X = int.Parse(positionAndOrientationParts[0]), Y = int.Parse(positionAndOrientationParts[1]) });

            WriteOutput("Please enter a string of commands for the spider e.g. FLFLFRFFLF");
            var commands = ReadInput();

            var commander = SpiderCommander.With().Wall(x, y).Spider(spider);
            commander.AddCommand(commands);
            commander.Execute();

            WriteOutput("Thie final output is:");
            WriteOutput(string.Format("{0} {1} {2}", spider.Position.X, spider.Position.Y, spider.Orientation));

            System.Console.ReadKey();
        }

        static void WriteOutput(string output)
        {
            System.Console.WriteLine(output);
        }

        static string ReadInput()
        {
            return System.Console.ReadLine();
        }
    }
}
