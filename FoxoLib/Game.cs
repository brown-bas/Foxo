using System.Timers;

namespace FoxoLib
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
                if (data.Count != 9 || !DateTime.TryParse(data[7], out DateTime t) || t > DateTime.Now) throw new SaveFileException();
                try
                {
                    //Name:0 HP:1 Happiness:2 Hunger:3 Energy:4 IsSleeping:5 LastFed:6 LastPet:7 SaveDate:8
                    Fomp = new(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), bool.Parse(data[5]), DateTime.Parse(data[6]), DateTime.Parse(data[7]), DateTime.Parse(data[8]));
                    SaveState();
                } catch (Exception)
                {
                    throw new SaveFileException();
                }
            }
            else {
                if (!Directory.Exists("./saves/")) Directory.CreateDirectory("./saves/");
                Fomp = new(fompName!, 3, 100, 0, 100, false, DateTime.Now, DateTime.Now, DateTime.Now);
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
            writer.Write($"Name:{Fomp.Name}\nHP:{Fomp.HP}\nHappiness:{Fomp.Happiness}\nHunger:{Fomp.Hunger}\nEnergy:{Fomp.Energy}\nIsSleeping:{Fomp.IsSleeping}\nLastFed:{Fomp.LastFed}\nLastPet:{Fomp.LastPet}\nSaveDate:{Fomp.SaveDate}");
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