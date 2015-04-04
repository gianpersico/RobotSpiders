using System;
namespace RobotSpiders.Domain
{
    public interface IAmATwoDimensionalSurface
    {
        Position OccupiedPosition { get; }
        int Width { get; }
        int Height { get; }
    }
}
