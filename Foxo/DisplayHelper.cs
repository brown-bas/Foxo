using FoxoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxo
{
    public class DisplayHelper
    {
        public static void PrintLogo()
        {
            Console.Clear();
            Console.WriteLine(" ________ ________  _____ ______   ________   \r\n|\\  _____\\\\   __  \\|\\   _ \\  _   \\|\\   __  \\  \r\n\\ \\  \\__/\\ \\  \\|\\  \\ \\  \\\\\\__\\ \\  \\ \\  \\|\\  \\ \r\n \\ \\   __\\\\ \\  \\\\\\  \\ \\  \\\\|__| \\  \\ \\   ____\\\r\n  \\ \\  \\_| \\ \\  \\\\\\  \\ \\  \\    \\ \\  \\ \\  \\___|\r\n   \\ \\__\\   \\ \\_______\\ \\__\\    \\ \\__\\ \\__\\   \r\n    \\|__|    \\|_______|\\|__|     \\|__|\\|__|   \r\n                                              \r\n                                              \r\n                                              ");
        }

        public static void PrintOptions(string[] options, bool slots) {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(slots ? "Slot " : "")}{i + 1} - {options[i]}");
            }
            Console.WriteLine();
        }

        /* TODO: bruh
        public static void WindowSizeChangeListener()
        {
            int initialWidth = Console.WindowWidth;
            int initialHeight = Console.WindowHeight;

            while (true)
            {
                if (Console.WindowWidth != initialWidth || Console.WindowHeight != initialHeight)
                {
                    Console.Clear();
                    switch (MenuHelper.CurrentMenu)
                    {
                        case "MainMenu":
                            MenuHelper.MainMenu();
                        break;
                        case "SlotMenu":
                            MenuHelper.SlotMenu();
                        break;
                        case "EmptySlotMenu":
                            MenuHelper.EmptySlotMenu(MenuHelper.CurrentMenuSlot);
                        break;
                        case "NonEmptySlotMenu":
                            MenuHelper.NonEmptySlotMenu(MenuHelper.CurrentMenuSlot);
                        break;
                        case "NewGameSlotMenu":
                            MenuHelper.NewGameSlotMenu();
                        break;
                        case "Game":
                            MenuHelper.CurrentGame.Fomp.Render();
                        break;
                    }

                    initialWidth = Console.WindowWidth;
                    initialHeight = Console.WindowHeight;
                }

                Thread.Sleep(500);
            }
        }*/
    }
}
