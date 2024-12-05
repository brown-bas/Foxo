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
        public static int UpdateFrequency { get; } = 600000;
        public Fomp Fomp { get; set; }
        public int SlotNum { get; set; }
        private System.Timers.Timer timer;
        public Game(int slotNum, string? fompName) {
            SlotNum = slotNum;

            if (File.Exists($"./saves/{SlotNum}.fomp") && fompName == null)
            {
                List<string> data = [];
                foreach (var row in File.ReadAllLines($"./saves/{SlotNum}.fomp")) data.Add(row[(row.IndexOf(':') + 1)..]);
                if (!DateTime.TryParse(data[5], out DateTime t) || t.Subtract(DateTime.Now) > TimeSpan.Zero) throw new SaveFileException();
                try
                {
                    Fomp = new(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[0], bool.Parse(data[4]), DateTime.Parse(data[5]), DateTime.Parse(data[6]));
                    SaveState();
                } catch (Exception)
                {
                    throw new SaveFileException();
                }
            }
            else {
                if (!Directory.Exists("./saves/")) Directory.CreateDirectory("./saves/");
                Fomp = new(3, 0, 100, fompName!, false, DateTime.Now, DateTime.Now);
                SaveState();
            }

            timer = new System.Timers.Timer(UpdateFrequency);
            timer.Elapsed += OnTimeElapsed!;
            timer.Start();

            Fomp.Render();
        }
        public void SaveState()
        {
            Fomp.SaveDate = DateTime.Now;
            StreamWriter writer = new($"./saves/{SlotNum}.fomp");
            writer.Write($"Name:{Fomp.Name}\nHP:{Fomp.HP}\nHunger:{Fomp.Hunger}\nEnergy:{Fomp.Energy}\nIsSleeping:{Fomp.IsSleeping}\nLastFed:{Fomp.LastFed}\nSaveDate:{Fomp.SaveDate}");
            writer.Close();
        }

        public void OnTimeElapsed(object source, ElapsedEventArgs e)
        {
            Fomp.Update();
            Fomp.Render();
            SaveState();
        }
    }
}
