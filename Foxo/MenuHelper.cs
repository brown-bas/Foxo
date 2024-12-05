using FoxoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxo
{
    public class MenuHelper
    {
        public static Game CurrentGame { get; private set; }
        public static string CurrentMenu { get; private set; } = "MainMenu";
        public static int CurrentMenuSlot { get; private set; }
        public static void MainMenu()
        {
            CurrentMenu = "MainMenu";
            CurrentMenuSlot = -1;
            DisplayHelper.PrintLogo();
            DisplayHelper.PrintOptions(["Játék betöltése / Új játék", "Kilépés"], false);

            int o = InputHelper.GetValidatedInput<int>("Szia! Kérlek válassz egy opciót (1-2): ", InputHelper.CreateValidationFuncForInt(1, 2));

            switch (o)
            {
                case 1:
                    SlotMenu();
                break;
                case 2:
                    return;
            }
        }
        public static void SlotMenu()
        {
            CurrentMenu = "SlotMenu";
            CurrentMenuSlot = -1;
            DisplayHelper.PrintLogo();
            if (Directory.Exists("./saves/"))
            {
                DisplayHelper.PrintOptions(
                    Enumerable.Range(1, 3)
                    .Select(i => File.Exists($"./saves/{i}.fomp") ? "Mentett játékállás észlelve" : "Üres")
                    .ToArray(),
                    true
                );

                int slot = InputHelper.GetValidatedInput<int>("Kérlek válassz egy slotot (1-3): ", InputHelper.CreateValidationFuncForInt(1, 3));

                if (!File.Exists($"./saves/{slot}.fomp")) EmptySlotMenu(slot);
                else NonEmptySlotMenu(slot);
            } else NewGameSlotMenu();
        }
        public static void EmptySlotMenu(int slot)
        {
            CurrentMenu = "EmptySlotMenu";
            CurrentMenuSlot = slot;
            DisplayHelper.PrintLogo();
            DisplayHelper.PrintOptions(["Új játék", "Kilépés"], false);

            int o = InputHelper.GetValidatedInput<int>("Kérlek válassz egy opciót (1-2): ", InputHelper.CreateValidationFuncForInt(1, 2));

            if (o == 2) return;
            else
            {
                DisplayHelper.PrintLogo();
                string fompName = InputHelper.GetValidatedInput<string>("Nevezd el a rókádat! (1-10 karakter): ", InputHelper.CreateValidationFuncForStr(0, 10));

                InitGame(slot, fompName);
            }
        }
        public static void NonEmptySlotMenu(int slot)
        {
            CurrentMenu = "NotEmptySlotMenu";
            CurrentMenuSlot = slot;
            DisplayHelper.PrintLogo();
            DisplayHelper.PrintOptions(["Játékállás betöltése", "Új játék", "Kilépés"], false);

            int o = InputHelper.GetValidatedInput<int>("Kérlek válassz egy opciót (1-3): ", InputHelper.CreateValidationFuncForInt(1, 3));


            switch (o)
            {
                case 1:
                    InitGame(slot, null);    
                break;
                case 2:
                    DisplayHelper.PrintLogo();
                    char s = InputHelper.GetValidatedInput<char>("Biztos, hogy új játékot szeretnél létrehozni? (Nem esetén kilép a program) I/N: ", InputHelper.CreateValidationFuncForChar(['I', 'Y', 'N', 'i', 'y', 'n']));

                    if (!new char[] { 'I', 'Y', 'i', 'y' }.Contains(s)) return;
                    else
                    {
                        DisplayHelper.PrintLogo();
                        string fompName = InputHelper.GetValidatedInput<string>("Nevezd el a rókádat! (1-10 karakter): ", InputHelper.CreateValidationFuncForStr(0, 10));
                        InitGame(slot, fompName);
                    }
                    break;
                case 3: return;
            }
        }
        public static void NewGameSlotMenu()
        {
            CurrentMenu = "NewGameSlotMenu";
            CurrentMenuSlot = 1;
            DisplayHelper.PrintLogo();
            string fompName = InputHelper.GetValidatedInput<string>("Szia! Úgy tűnik, először játszol ezzel a játékkal, így a játékállásod az első slot-ra lesz elmentve.\nNevezd el a rókádat! (1-10 karakter): ", InputHelper.CreateValidationFuncForStr(0, 10));
            InitGame(1, fompName);
        }
        public static void InitGame(int slot, string? fompName)
        {
            try
            {
                CurrentMenu = "Game";
                Console.Clear();
                Game game = new(slot, fompName);
                Fomp fomp = game.Fomp;
                CurrentGame = game;
                Console.CursorVisible = false;
                while (!fomp.IsDead)
                {
                    char userInput = Console.ReadKey(true).KeyChar;

                    switch(userInput)
                    {
                        case 'f':
                            fomp.Feed();
                            game.SaveState();
                        break;
                        case 's':
                            fomp.ToggleSleep();
                            game.SaveState();
                        break;
                        case 'q':
                            game.SaveState();
                            return;
                        break;
                    }
                }
                Console.Beep(); //beep :3
            }
            catch (SaveFileException error) { Console.WriteLine(error.Message); }
        }
    }
}
