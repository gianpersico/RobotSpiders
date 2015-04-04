using System;

namespace RobotSpiders.Domain
{
    public interface ISpider
    {
        Orientation Orientation { get; }
        Position Position { get; }
        void RotateLeft();
        void RotateRight();
        void MoveForward();
    }
}
