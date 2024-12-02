using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Foxo
{
    public class Game
    {
        public Fomp Fomp { get; set; }
        public int SlotNum { get; set; }
        private System.Timers.Timer timer;
        public Game(int slotNum, string? fompName) {
            this.SlotNum = slotNum;

            Console.Clear();

            if (File.Exists($"./saves/{SlotNum}.fomp") && fompName == null)
            {
                List<string> data = [];
                foreach (var row in File.ReadAllLines($"./saves/{SlotNum}.fomp")) data.Add(row[(row.IndexOf(':') + 1)..]);
                if (!DateTime.TryParse(data[5], out DateTime t) || t.Subtract(DateTime.Now) > TimeSpan.Zero) throw new SaveFileException();
                Fomp = new(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[0], bool.Parse(data[4]), DateTime.Parse(data[5]));
            }
            else {
                if (!Directory.Exists("./saves/")) Directory.CreateDirectory("./saves/");
                Fomp = new(3, 0, 100, fompName!, false, DateTime.Now);
                SaveState();
            }

            timer = new System.Timers.Timer(1000);  // 300,000 ms = 5 minutes
            timer.Elapsed += OnTimeElapsed!;
            timer.Start();

            Fomp.ToString();
        }
        public void SaveState()
        {
            Fomp.SaveDate = DateTime.Now;
            StreamWriter writer = new($"./saves/{SlotNum}.fomp");
            writer.Write($"Name:{Fomp.Name}\nHP:{Fomp.HP}\nHunger:{Fomp.Hunger}\nEnergy:{Fomp.Energy}\nIsSleeping:{Fomp.IsSleeping}\nSaveDate:{Fomp.SaveDate}");
            writer.Close();
        }

        public void OnTimeElapsed(object source, ElapsedEventArgs e)
        {
            Fomp.Update();
            SaveState();
        }
    }
}
