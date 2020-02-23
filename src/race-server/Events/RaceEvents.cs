﻿using System.Collections.Generic;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace SSC.Server
{
    public class RaceEvents
    {
        delegate void RaceHosted(Player player, string track, string car, int laps);
        delegate void RaceJoined(Player player, string code);

        private Dictionary<string, RaceInstance> RaceInstances = new Dictionary<string, RaceInstance>();

        public RaceEvents()
        {
            RaceServer rs = RaceServer.Instance;
            rs.EventWrapper.RegisterEvent<RaceHosted>(OnRaceHosted);
            rs.EventWrapper.RegisterEvent<RaceJoined>(OnRaceJoined);
        }

        public void OnRaceHosted([FromSource]Player player, string trackName, string carName, int laps)
        {
            Debug.WriteLine($"{nameof(RaceEvents)} - Hosting race with track {trackName} with car {carName} for {laps} laps.");

            //TODO: Preload all the available tracks, so we can preemtively deny invalid ones.
            //      Also comes in handy when we need to enumerate them all for like a selection menu.

            //TODO: if track is not valid then reject.

            if (laps <= 0)
            {
                //TODO: Notify the command invoker something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} - OnRaceHosted: Invalid lap count: {laps}");
                return;
            }

            //TODO: Do a check client-side to see if the vehicle requested is valid. Otherwise we don't annoy the server with it.
            //      Server can't check this unless we have JuanSink.

            string raceCode = StringHelper.GetRandomHexString(4);
            RaceInstance instance = new RaceInstance(trackName, carName, laps);

            //TODO: Check if the race isn't overlapping another track (IsRacePhysciallyPossible).
            RaceInstances.Add(raceCode, instance);

            Debug.WriteLine($"New race created race with RaceID {raceCode}");
        }

        public void OnRaceJoined([FromSource]Player player, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Invalid RID: {code}");
                return;
            }

            bool foundRace = RaceInstances.TryGetValue(code, out RaceInstance ri);

            if (!foundRace)
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Could not find a race with RID {code}");
                return;
            }

            if (!ri.IsJoinable)
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Race is already closed. RID: {code}");
                return;
            }

            ri.AssignPlayer(player);
        }
    }
}
 