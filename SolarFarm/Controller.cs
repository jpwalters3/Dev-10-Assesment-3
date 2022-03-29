using System;
using System.Collections.Generic;
using System.Text;
using SolarFarm.CORE;
using SolarFarm.BLL;

namespace SolarFarm
{
    internal class Controller
    {
        RecordService service;
        internal void Run()
        {
            service = new RecordService();
            int userInput = -1;

            while (userInput != 0)
            {
                ConsoleIO.PrintMenu();
                userInput = ConsoleIO.GetUserInt("Select [0-4]: ", 0, 4);
                switch (userInput)
                {
                    case 0:
                        Console.WriteLine("Exit");
                        break;
                    case 1:
                        ViewSection();
                        break;
                    case 2:
                        AddPanel();
                        break;
                    case 3:
                        UpdatePanel();
                        break;
                    case 4:
                        RemovePanel();
                        break;
                }
            }
        }

        private void AddPanel()
        {
            Panel panel = new Panel(
                ConsoleIO.GetUserString("Enter the section name: "),
                ConsoleIO.GetUserInt("Enter Row: ", 0, 250),
                ConsoleIO.GetUserInt("Enter Column: ", 0, 250),
                ConsoleIO.GetUserInt("Enter Year: "),
                ConsoleIO.GetUserMats(),
                ConsoleIO.GetUserBool("Tracking (y/n): "));

            Result<Panel> result = service.AddPanel(panel);

            if (!result.Success) ConsoleIO.PrintError(result.Message);
            else ConsoleIO.Display("Panel Successfuly added!");
        }

        private void ViewSection()
        {
            string section = ConsoleIO.GetUserString("Enter the section name: ");
            Result<List<Panel>> result = service.GetSection(section);

            if (result.Success)
            {
                ConsoleIO.Display("\n---------------------------");
                ConsoleIO.Display("PANELS IN " + section.ToUpper());
                ConsoleIO.Display("---------------------------\n");

                foreach(Panel panel in result.Data)
                {
                    ConsoleIO.Display(panel.ToString() + "\n");
                }
            }
            else
            {
                ConsoleIO.PrintError(result.Message);
            }
        }

        private void RemovePanel()
        {
            Panel panel = new Panel(
                ConsoleIO.GetUserString("Enter the section name: "),
                ConsoleIO.GetUserInt("Enter Row: ", 0, 250),
                ConsoleIO.GetUserInt("Enter Column: ", 0, 250),
                0,0,false);

            Result<Panel> result = service.RemovePanel(panel);
            if (result.Success) ConsoleIO.Display(result.Message);
            else ConsoleIO.PrintError(result.Message);
        }

        private void UpdatePanel()
        {
            string KeySection;
            int KeyRow, KeyCol;

            Panel update = ConsoleIO.GetKey();
            KeySection = update.Section;
            KeyRow = update.Row;
            KeyCol = update.Col;

            ConsoleIO.Display($"Editing {KeySection}-{KeyRow}-{KeyCol}");
            ConsoleIO.Display("Press [Enter] to keep original value");

            ConsoleIO.GetUpdateData(update);

            Result<Panel> result = service.UpdatePanel(KeySection, KeyRow, KeyCol, update);
            if (result.Success) ConsoleIO.Display(result.Message);
            else ConsoleIO.PrintError(result.Message);
        }
    }
}
