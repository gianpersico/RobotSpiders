using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotSpiders.Domain
{
    public class Spider : RobotSpiders.Domain.ISpider
    {
        public Orientation Orientation { get; private set; }
        public Position Position { get; private set; }

        private Spider(Orientation orientation)
        {
            Orientation = orientation;
            Position = new Position { X = 0, Y = 0 };
        }

        private Spider(Orientation orientation, Position position)
        {
            Orientation = orientation;
            Position = position;
        }

        public static Spider CreateDefault()
        {
            return new Spider(Orientation.Up, new Position { X = 0, Y = 0 });
        }

        public static Spider WithStartOrientationAndPosition(Orientation orientation, Position position)
        {
            return new Spider(orientation, position);
        }

        public void RotateRight()
        {
            Orientation = Orientation == Orientation.Left ? Orientation.Up : Orientation + 1;
        }

        public void RotateLeft()
        {
            Orientation = Orientation == Orientation.Up ? Orientation.Left : Orientation - 1;
        }

        public void MoveForward()
        {
            var currentX = Position.X;
            var currentY = Position.Y;

            switch (Orientation)
            {
                case Orientation.Up:
                    currentY += 1;
                    break;
                case Orientation.Right:
                    currentX += 1;
                    break;
                case Orientation.Down:
                    currentY -= 1;
                    break;
                case Orientation.Left:
                    currentX -= 1;
                    break;
            }
            Position = new Position { X = currentX, Y = currentY };
        }
    }

    //public interface ISpiderBuilder
    //{
    //    ISpiderBuilder WithStartOrientation(Orientation orientation);
    //    ISpiderBuilder WithStartPosition(Position position);
    //    ISpider Build();
    //    ISpider BuildDefault();
    //}

    //public class SpiderBuilder : ISpiderBuilder
    //{
    //    private Orientation _startOrientation;
    //    private Position _position;

    //    public ISpiderBuilder WithStartOrientation(Orientation orientation)
    //    {
    //        _startOrientation = orientation;
    //        return this;
    //    }

    //    public ISpiderBuilder WithStartPosition(Position position)
    //    {
    //        _position = position;
    //        return this;
    //    }

    //    public ISpider Build()
    //    {
    //        return new Spider(_startOrientation, _position);
    //    }
    //}

    public enum Orientation
    {
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4
    }
}
