using System;
using System.Collections.Generic;
using System.IO;

namespace DiscoveringMars
{
    class Program
    {

        static void CW(string value)
        {
            Console.WriteLine(value);
        }

        static void Main(string[] args)
        {
            string initialPosition = string.Empty;
            string upperRightCoordinate = string.Empty;
            string commands = string.Empty;

            var rovers = new List<Rover>();

            while (true)
            {
                PrintMainMeu();

                var key = Console.ReadLine();
                switch (key.ToUpper())
                {
                    case "F":
                        CW("Please specify file path:");
                        var filePath = Console.ReadLine();
                        if (!File.Exists(filePath))
                            break;

                        string[] lines = File.ReadAllLines(filePath);
                        upperRightCoordinate = lines[0];
                        for (int i = 1; i < lines.Length; i += 2)
                        {
                            initialPosition = lines[i];
                            commands = lines[i + 1];
                            rovers.Add(new Rover(initialPosition, commands, upperRightCoordinate));
                        }

                        break;
                    case "R":
                        CW("Please enter Upper Right Coordinates: ");
                        upperRightCoordinate = Console.ReadLine();
                        while (true)
                        {
                            CW("Please enter rover's initial position (Q for quit)");
                            initialPosition = Console.ReadLine();
                            if (initialPosition.ToUpper() == "Q")
                                break;

                            CW("Please enter commands: ");
                            commands = Console.ReadLine();
                            rovers.Add(new Rover(initialPosition, commands, upperRightCoordinate));
                        }

                        break;
                    case "Q":
                        Environment.Exit(0);
                        break;
                }

                for (int i = 0; i < rovers.Count; i++)
                {
                    var r = rovers[i];
                    r.ProcessCommands();
                    CW($"Current Position for Rover[{i}]: ({r.GetCurrentPosition()})");
                    CW("Execution Steps:");
                    CW(r.GetCommandHistory());

                }

                rovers.Clear();
            }
        }


        /// <summary>
        /// Displays main menu
        /// </summary>
        static void PrintMainMeu()
        {
            CW("Please select your choice:");
            CW("F: Read from file");
            CW("R: Read from console");
            CW("Q: Exit ");
        }

    }
}
