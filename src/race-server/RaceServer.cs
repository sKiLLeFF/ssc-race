using System;

using CitizenFX.Core;

using SSC.Shared.Wrappers;
using SSC.Shared.Static;

using static CitizenFX.Core.Native.API;
using static SSC.Shared.Static.RaceStatic;

namespace SSC.Server
{
    public class RaceServer : BaseScript
    {
        public static RaceServer Instance { get; private set; }
        public RaceEventCollection Events { get; private set; }

        private RaceEvents raceEvents;
        private ChatEvents chatEvents;

        //private readonly RaceCollection RaceCollection = new RaceCollection();

        public RaceServer()
        {
            Instance = this;

            Events = new RaceEventCollection(
                EventHandlers.Add, TriggerEvent, TriggerClientEvent
            );

            chatEvents = new ChatEvents();
            raceEvents = new RaceEvents();

        }

        //public void SaveTrack(string name, string json)
        //{
        //    Debug.WriteLine($"{nameof(RaceServer)} Saving new track: {name}");
        //    SaveResourceFile(GetCurrentResourceName(), $"tracks/{name}.json", json, -1);

        //    EventCollection.InvokeEvent<EventClientNotificationAccepted>(true, "SaveTrack");
        //}
    }
}
