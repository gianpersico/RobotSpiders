using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpiders.Domain
{
    public class Wall : IAmATwoDimensionalSurface
    {
        public Position OccupiedPosition { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public Wall(int width, int height)
        {
            Width = width;
            Height = height;
            OccupiedPosition = new Position { X = 0, Y = 0 };
        }
    }    
}
