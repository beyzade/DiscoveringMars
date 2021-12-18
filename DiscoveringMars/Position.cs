using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoveringMars
{

    /// <summary>
    /// Enum for defining direction N:North, E: East, S: South, W:West 
    /// </summary>
    public enum Direction
    {
        N,
        E,
        S,
        W
    }

    /// <summary>
    /// Defines position including X and Y coordinates and direction
    /// </summary>
    public class Position : Coordinate
    {
        public Direction Direction { get; set; }

        public override string ToString()
        {
            return $"({X} {Y} {Direction})";
        }

        public Position()
        {

        }
        public Position(int x, int y, Direction direction) : base(x, y)
        {
            Direction = direction;
        }
    }
}
