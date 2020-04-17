using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;
using CitizenFX.Core;

namespace SSC.Client.Race
{
    public enum RaceState
    {
        Loading,
        Gridding,
        Started,
        Finishing,
        Closed
    }

    public class Race2
    {
        private string RaceName = "Unknown";

        private List<RaceStart2> StartingPoints = new List<RaceStart2>();
        private List<RaceCheckpoint2> Checkpoints = new List<RaceCheckpoint2>();
        private List<Racer2> Racers = new List<Racer2>();

        private bool Debug = false;

        public Race2(string name, bool debug)
        {
            RaceName = name;
            Debug = debug;
        }

        public async Task CreatorUpdate()
        {
            foreach (RaceCheckpoint2 checkpoint in Checkpoints)
            {
                await checkpoint.Render();
            }

            if (Debug)
            {
                foreach (RaceStart2 start in StartingPoints)
                {
                    start.Render();
                }
            }
        }

        public async Task RaceUpdate()
        {
            await BaseScript.Delay(0);
        }

        public void Load()
        {
        }

        public void Save()
        {
            object trackDataObject = new object[] {
                StartingPoints,
                Checkpoints
            };

            BaseScript.TriggerServerEvent("ssrc.race::savetrack", RaceName, JsonConvert.SerializeObject(trackDataObject));
        }

        public void AddCheckpoint(RaceCheckpoint2 cp)
        {
            Checkpoints.Add(cp);
        }

        public void AddStart(RaceStart2 start)
        {
            StartingPoints.Add(start);
        }
    }
}
