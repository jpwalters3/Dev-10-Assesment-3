using System;
using System.Collections.Generic;
using System.Text;
using SolarFarm.CORE;

namespace SolarFarm
{
    internal class ConsoleIO
    {
        internal static void PrintError(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        internal static void Display(string message)
        {
            Console.WriteLine(message);
        }
        internal static void PrintMenu()
        {
            Console.WriteLine("                  MENU ");
            Console.WriteLine("+------------------------------------+");
            Console.WriteLine("|               0. Exit              |");
            Console.WriteLine("|      1. Find Panels by Section     |");
            Console.WriteLine("|           2. Add a Panel           |");
            Console.WriteLine("|          3. Update a Panel         |");
            Console.WriteLine("|          4. Remove a Panel         |");
            Console.WriteLine("+------------------------------------+");
        }
        public static int GetUserInt(string message, int low = int.MinValue, int high = int.MaxValue)
        {
            int result = 0;

            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= low && result <= high) return result;
                    else Console.WriteLine("Error: input out of range");
                }
                else Console.WriteLine("Error: invalid input");
            }
        }

        public static bool GetUserBool(string message)
        {
            Console.Write(message);
            return Console.ReadLine().ToUpper() == "Y";
        }

        public static string GetUserString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static Materials GetUserMats()
        {
            int userInput = 0;
            Console.WriteLine("Materials:" +
            "\n1. Multicrystalline Silicon" +
            "\n2. Monocrystalline Silicon" +
            "\n3. Amorphous Silicon" +
            "\n4. Cadmuim Telluride" +
            "\n5. Copper Indium Gallium Selenide");
            
            while (true)
            {
                userInput = GetUserInt("Select[1-5]: ", 1, 5);
                switch (userInput)
                {
                    case 1:
                        return Materials.MULTI;
                    case 2:
                        return Materials.MONO;
                    case 3:
                        return Materials.AMORPH;
                    case 4:
                        return Materials.CADT;
                    case 5:
                        return Materials.COPS;
                }
            }
        }

        internal static Panel GetKey()
        {
            Panel result = new Panel();
            result.Section = GetUserString("Enter the Current Section Name: ");
            result.Row = GetUserInt("Enter the Current Row Number: ");
            result.Col = GetUserInt("Enter the Current Column Number: ");

            return result;
        }

        internal static void GetUpdateData(Panel toUpdate)
        {
            int tempInt;

            Console.Write($"Section ({toUpdate.Section}): ");
            toUpdate.Section = Console.ReadLine();

            Console.Write($"Row ({toUpdate.Row}): ");
            if (int.TryParse(Console.ReadLine(), out tempInt))
            {
                toUpdate.Row = tempInt;
            }
            else toUpdate.Row = int.MinValue;

            Console.Write($"Column ({toUpdate.Col}): ");

            if (int.TryParse(Console.ReadLine(), out tempInt))
            {
                toUpdate.Col = tempInt;
            }
            else toUpdate.Col = int.MinValue;

            Console.Write("Year: ");

            if (int.TryParse(Console.ReadLine(), out tempInt))
            {
                toUpdate.Year = tempInt;
            }
            else toUpdate.Year = int.MinValue;

            toUpdate.Material = GetUserMats();

            toUpdate.IsTracking = GetUserBool("Tracking? (y/n): ");

        }
    }
}
