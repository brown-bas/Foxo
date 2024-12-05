using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Foxo
{
    public class Fomp
    {
        public int HP { get; set; }
        public int Hunger { get; set; }
        public int Energy { get; set; }
        public string Name { get; set; }
        public bool IsDead { get; set; }
        public bool IsSleeping { get; set; }
        public static int Timeout { get; } = 5000;
        public DateTime LastFed { get; set; }
        public DateTime SaveDate { get; set; }

        public Fomp(int hp, int hunger, int energy, string name, bool isSleeping, DateTime lastFed, DateTime saveDate)
        {
            HP = hp;
            Hunger = hunger;
            Energy = energy;
            Name = name;
            SaveDate = saveDate;
            IsDead = HP <= 0;
            IsSleeping = isSleeping;

            for (int i = 0; i < Math.Round((DateTime.Now - saveDate).TotalMilliseconds / Game.UpdateFrequency); i++) Update();
            LastFed = lastFed;
        }
        public void Update()
        {
            if (!IsDead)
            {
                if (Hunger >= 100)
                {
                    HP--;
                    Hunger = 0;
                } else
                {
                    Hunger += !IsSleeping ? 2 : 1;
                }

                if (!IsSleeping)
                {
                    if (Energy <= 0)
                    {
                        HP--;
                        Energy = 100;
                    } else
                    {
                        Energy--;
                    }
                } else
                {
                    if(Energy < 100)
                    {
                        Energy++;
                    } else
                    {
                        IsSleeping = false;
                    }
                }
                IsDead = HP <= 0;
            }
        }

        public void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            
            string topBarText = IsDead ? $"{Name} - Elpusztult" : $"{Name}\t|\tÉleterő: {new string('*', HP) + new string('-', 3-HP)}\t|\tÉhség: {Hunger:000}\t|\tEnergia: {Energy:000}".Replace("\t", new string(' ', 5));
            Console.SetCursorPosition((Console.WindowWidth/2)-topBarText.Length/2, 0);
            Console.WriteLine(topBarText);

            string moodExpression;
            string? moodString = null;

            Console.OutputEncoding = Encoding.UTF8;
            if (!IsSleeping)
            {
                if(Hunger > 40)
                {
                    moodExpression = "/ᐠ.ꞈ.ᐟ\\";
                    moodString = "Éhes vagyok!";
                } else if (Energy < 40)
                {
                    moodExpression = "/ᐠ.ꞈ.ᐟ\\";
                    moodString = "Álmos vagyok!";
                } else {
                    moodExpression = "/ᐠ • ⩊ • マ";
                }
            } else
            {
                moodExpression = "/ᐠ˵- ᴗ -˵マ";
                moodString = " ᶻ 𝗓 𐰁";
                if(Energy == 100) IsSleeping = false;
            }

            if(Hunger == 100 || Energy == 0)
            {
                moodExpression = "/ᐠ > ~ < マ";
                moodString = null;
            }

            if (IsDead) moodExpression = "/ᐠ x - x マ";

            Console.SetCursorPosition(0, Console.WindowHeight / 2);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition((Console.WindowWidth / 2) - moodExpression.Length / 2, Console.WindowHeight / 2);
            Console.WriteLine($"{moodExpression}{(moodString != null ? $" - {moodString}" : "")}");
        }

        public void Feed()
        {
            if (Hunger >= 5 && !IsSleeping && (DateTime.Now - LastFed).TotalMilliseconds > Timeout)
            {
                Hunger -= 5;
                LastFed = DateTime.Now;
                Render();
            }
        }

        public void ToggleSleep()
        {
            IsSleeping = !IsSleeping;
            Render();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void CheckStatus()
        {
            throw new NotImplementedException();
        }

        public void PassTime()
        {
            throw new NotImplementedException();
        }

        public void DisplayDetails()
        {
            throw new NotImplementedException();
        }
    }
}
