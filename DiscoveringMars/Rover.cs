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
    /// Defines X and Y coordinates
    /// </summary>
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate()
        {

        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Defines position including X and Y coordinates and direction
    /// </summary>
    public class Position:Coordinate
    {
        public Direction Direction { get; set; }

        public override string ToString()
        {
            return $"({X} {Y} {Direction})";
        }

        public Position()
        {

        }
        public Position(int x, int y, Direction direction): base(x,y)
        {
            Direction = direction;
        }
    }


    /// <summary>
    /// Defines a rover which has initial position and final position after executing commands send by NASA
    /// </summary>
    public class Rover
    {
        public Position InitialPosition { get; private set; }
        public Position CurrentPosition { get; private set; }

        private Coordinate UpperRightCoordinate;

        private string Commands;

        private StringBuilder Sb;

        /// <summary>
        /// Default constructor used when no parameter is specified at the time of creation. Initial position is defaulted to (0 0 N) and Upper right coordinate is defaulted to (5,5)
        /// </summary>
        public Rover()
        {
            InitialPosition = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.N
            };

            CurrentPosition = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.N
            };

            UpperRightCoordinate = new Coordinate { X = 5, Y = 5 };
            Sb = new StringBuilder().AppendFormat("Initial Position: {0}, Commands: {1}", InitialPosition, Commands).AppendLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialPosition">Initial position of rover, i.e 1 2 N</param>
        /// <param name="commands">Commands to be executed by rover i.e LMLMLMLMM</param>
        /// <param name="upperRightCoordinates">Upper right coordinates of plateau to be checked</param>
        public Rover(string initialPosition, string commands, string upperRightCoordinates)
        {
            var coordinates = upperRightCoordinates.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            UpperRightCoordinate = new Coordinate(Convert.ToInt32(coordinates[0]), Convert.ToInt32(coordinates[1]));

            Commands = commands;

            var position = initialPosition.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int x = Convert.ToInt32(position[0]);
            int y = Convert.ToInt32(position[1]);
            string direction = position[2];

            InitialPosition = new Position
            {
                X = x,
                Y = y,
                Direction = (Direction)Enum.Parse(typeof(Direction), direction)
            };

            CurrentPosition = new Position
            {
                X = x,
                Y = y,
                Direction = (Direction)Enum.Parse(typeof(Direction), direction)
            };

            Sb = new StringBuilder().AppendFormat("Initial Position: {0}, Commands: {1}", InitialPosition, Commands).AppendLine();

        }

        /// <summary>
        /// After rover is initialized, commands are executed step by step. If a command results in crossing border, it will be bypassed
        /// </summary>
        public void ProcessCommands()
        {
            foreach (var command in Commands)
            {
                var newPosition = GetNextPosition(new Position(CurrentPosition.X,CurrentPosition.Y, CurrentPosition.Direction) , command.ToString());
                if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X > UpperRightCoordinate.X || newPosition.Y > UpperRightCoordinate.Y)
                {
                    Sb.AppendFormat("Command bypassed due to crossing border, {0}: {1}", command, newPosition).AppendLine();
                }
                else
                {
                    CurrentPosition = newPosition;
                    Sb.AppendFormat("{0}: {1}", command, CurrentPosition).AppendLine();
                }
            }
        }

        /// <summary>
        /// Executes command according to the current position of a rover.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="command">Command to be executed</param>
        /// <returns></returns>
        Position GetNextPosition(Position current, string command)
        {
            switch (command)
            {
                case "L":
                case "R":
                    switch (current.Direction)
                    {
                        case Direction.N:
                            current.Direction = command == "L" ? Direction.W : Direction.E;
                            break;
                        case Direction.E:
                            current.Direction = command == "L" ? Direction.N : Direction.S;
                            break;
                        case Direction.S:
                            current.Direction = command == "L" ? Direction.E : Direction.W;
                            break;
                        case Direction.W:
                            current.Direction = command == "L" ? Direction.S : Direction.N;
                            break;
                    }

                    break;
                case "M":
                    switch (current.Direction)
                    {
                        case Direction.N:
                            current.Y++;
                            break;
                        case Direction.E:
                            current.X++;
                            break;
                        case Direction.S:
                            current.Y--;
                            break;
                        case Direction.W:
                            current.X--;
                            break;
                    }
                    break;
            }

            return current;
        }

        /// <summary>
        /// Changes the direction according to the command
        /// </summary>
        /// <param name="current">Current direction</param>
        /// <param name="command">Command to be exeuted</param>
        /// <returns></returns>
        Direction ChangeDirection(Direction current, string command)
        {
            return (Direction)Math.Abs((int)current + (command == "L" ? -1 : 1));
        }

        public string GetCurrentPosition()
        {
            return $"{CurrentPosition.X} {CurrentPosition.Y} {CurrentPosition.Direction}";
        }


        /// <summary>
        /// Prints out the execution history of the current rover including commands and resulting position
        /// </summary>
        /// <returns></returns>
        public string GetCommandHistory()
        {
            return Sb.ToString();
        }
    }
}
