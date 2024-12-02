using Foxo;
Console.CursorVisible = true;
Console.BackgroundColor = ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.Black;

// I know this code is kind of garbage...

PrintLogo();
Console.WriteLine("1 - Játék betöltése / Új játék\n2 - Kilépés");

Console.Write("\nSzia! Kérlek válassz egy opciót (1-2): ");

int o;
while (!int.TryParse(Console.ReadLine()!, out o) || o <= 0 || o > 2) Console.Write("Nem megfelelő bemenet!\n\nSzia! Kérlek válassz egy opciót (1-2): ");

// bleeeh (a somewhat okay-ish solution (it's so not lmao, but it's whatever for now))
switch (o)
{
    case 1:
        PrintLogo();
        if (Directory.Exists("./saves/"))
        {
            for (int i = 1; i <= 3; i++) Console.WriteLine($"Slot {i} - {(File.Exists($"./saves/{i}.fomp") ? "Mentett játékállás észlelve" : "Üres")}");

            Console.Write("\nKérlek válassz egy slotot (1-3): ");

            int slot;
            while (!int.TryParse(Console.ReadLine()!, out slot) || slot <= 0 || slot > 3) Console.Write("Nem megfelelő bemenet!\n\nKérlek válassz egy slotot (1-3): ");

            if (!File.Exists($"./saves/{slot}.fomp"))
            {
                PrintLogo();
                Console.WriteLine("1 - Új játék");
                Console.WriteLine("2 - Kilépés");

                Console.Write("\nKérlek válassz egy opciót (1-2): ");

                while (!int.TryParse(Console.ReadLine()!, out o) || o <= 0 || o > 2) Console.Write("Nem megfelelő bemenet!\n\nKérlek válassz egy opciót (1-2): ");

                if (o == 2) return;
                else
                {
                    string fompName = "";

                    PrintLogo();
                    Console.Write("Nevezd el a rókádat! (1-10 karakter): ");
                    fompName = Console.ReadLine()!;

                    while (fompName.Length > 10 || fompName.Length <= 0)
                    {
                        PrintLogo();
                        Console.Write("Hibás bemenetet adtál, próbáld meg máshogy elnevezni a rókádat! (1-10 karakter): ");
                        fompName = Console.ReadLine()!;
                    }

                    try
                    {
                        Game game = new(slot, fompName);
                        Fomp fomp = game.Fomp;
                        Console.CursorVisible = false;
                        while (!fomp.IsDead)
                        {
                            char userInput = Console.ReadKey().KeyChar;
                        }
                    }
                    catch (SaveFileException error) { Console.WriteLine(error.Message); }
                }
            }
            else
            {
                PrintLogo();
                Console.WriteLine("1 - Játékállás betöltése");
                Console.WriteLine("2 - Új játék");
                Console.WriteLine("3 - Kilépés");

                Console.Write("\nKérlek válassz egy opciót (1-3): ");

                while (!int.TryParse(Console.ReadLine()!, out o) || o <= 0 || o > 3) Console.Write("Nem megfelelő bemenet!\n\nKérlek válassz egy opciót (1-3): ");

                switch (o)
                {
                    case 1:
                        try
                        {
                            Game game = new(slot, null);
                            Fomp fomp = game.Fomp;
                            Console.CursorVisible = false;
                            while (!fomp.IsDead)
                            {
                                char userInput = Console.ReadKey().KeyChar;
                            }
                        }
                        catch (SaveFileException error) { Console.WriteLine(error.Message); }
                    break;
                    case 2:
                        PrintLogo();
                        Console.Write("Biztos, hogy új játékot szeretnél létrehozni? (Nem esetén kilép a program) I/N: ");

                        char s;
                        while (!char.TryParse(Console.ReadLine()!, out s) || !new char[] { 'I', 'Y', 'N', 'i', 'y', 'n' }.Contains(s))
                        {
                            PrintLogo();
                            Console.Write("Biztos, hogy új játékot szeretnél létrehozni? (Nem esetén kilép a program) I/N: ");
                        }
                        
                        if(new char[] { 'I', 'Y', 'i', 'y' }.Contains(s))
                        {
                            string fompName = "";

                            PrintLogo();
                            Console.Write("Nevezd el a rókádat! (1-10 karakter): ");
                            fompName = Console.ReadLine()!;

                            while (fompName.Length > 10 || fompName.Length <= 0)
                            {
                                PrintLogo();
                                Console.Write("Hibás bemenetet adtál, próbáld meg máshogy elnevezni a rókádat! (1-10 karakter): ");
                                fompName = Console.ReadLine()!;
                            }

                            try
                            {
                                Game game = new(slot, fompName);
                                Fomp fomp = game.Fomp;
                                Console.CursorVisible = false;
                                while (!fomp.IsDead)
                                {
                                    char userInput = Console.ReadKey().KeyChar;
                                }
                            }
                            catch (SaveFileException error) { Console.WriteLine(error.Message); }
                        } else return;
                    break;
                    case 3: return;
                }
            }
        }
        else
        {
            string fompName = "";

            PrintLogo();
            Console.Write("Szia! Úgy tűnik, először játszol ezzel a játékkal, így a játékállásod az első slot-ra lesz elmentve.\nNevezd el a rókádat! (1-10 karakter): ");
            fompName = Console.ReadLine()!;

            while (fompName.Length > 10 || fompName.Length <= 0)
            {
                PrintLogo();
                Console.Write("Szia! Úgy tűnik, először játszol ezzel a játékkal, így a játékállásod az első slot-ra lesz elmentve.\nHibás bemenetet adtál, próbáld meg máshogy elnevezni a rókádat! (1-10 karakter): ");
                fompName = Console.ReadLine()!;
            }

            try
            {
                Game game = new(1, fompName);
                Fomp fomp = game.Fomp;
                string a = Console.ReadLine()!;
            }
            catch (SaveFileException error) { Console.WriteLine(error.Message); }
        }
    break;
    case 2:
        return;
}

// I thought this is a more elegant way of printing the menu header while clearing the previous options
static void PrintLogo()
{
    Console.Clear();
    Console.WriteLine(" ________ ________  _____ ______   ________   \r\n|\\  _____\\\\   __  \\|\\   _ \\  _   \\|\\   __  \\  \r\n\\ \\  \\__/\\ \\  \\|\\  \\ \\  \\\\\\__\\ \\  \\ \\  \\|\\  \\ \r\n \\ \\   __\\\\ \\  \\\\\\  \\ \\  \\\\|__| \\  \\ \\   ____\\\r\n  \\ \\  \\_| \\ \\  \\\\\\  \\ \\  \\    \\ \\  \\ \\  \\___|\r\n   \\ \\__\\   \\ \\_______\\ \\__\\    \\ \\__\\ \\__\\   \r\n    \\|__|    \\|_______|\\|__|     \\|__|\\|__|   \r\n                                              \r\n                                              \r\n                                              ");
}