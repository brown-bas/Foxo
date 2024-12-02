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
        public DateTime SaveDate { get; set; }

        public Fomp(int hp, int hunger, int energy, string name, bool isSleeping, DateTime saveDate)
        {
            HP = hp;
            Hunger = hunger;
            Energy = energy;
            Name = name;
            SaveDate = saveDate;
            IsDead = HP <= 0;
            IsSleeping = isSleeping;
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
                ToString();
            }
        }

        public override string ToString()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            
            string topBarText = IsDead ? $"\t{Name} - Elpusztult" : $"\t{Name}\t|\tÉleterő: {HP}\t|\tÉhség: {Hunger}\t|\tEnergia: {Energy}";
            Console.SetCursorPosition((Console.WindowWidth/2)-topBarText.Length, 0);
            Console.WriteLine(topBarText);

            string moodString = "";

            Console.OutputEncoding = Encoding.UTF8;
            if (!IsSleeping)
            {
                if(Hunger > 40)
                {
                    moodString = "\t\t/ᐠ.ꞈ.ᐟ\\ - Éhes vagyok!";
                } else if (Energy < 40)
                {
                    moodString = "\t\t/ᐠ.ꞈ.ᐟ\\ - Álmos vagyok!";
                } else {
                    moodString = "/ᐠ • ⩊ • マ";
                }
            } else
            {
                moodString = "/ᐠ˵- ᴗ -˵マ ᶻ 𝗓 𐰁";
            }

            if (IsDead) moodString = "/ᐠ x - x マ";

            Console.SetCursorPosition(0, Console.WindowHeight / 2);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition((Console.WindowWidth / 2) - moodString.Length, Console.WindowHeight / 2);
            Console.WriteLine(moodString);

            return topBarText;
        }

        public void Feed()
        {
            throw new NotImplementedException();
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
