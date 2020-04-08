using CitizenFX.Core;

using SSC.Shared.Wrappers;

using SSC.Client.Events;
using SSC.Client.Commands;
using SSC.Client.States;
using SSC.Client.Worker;

using static CitizenFX.Core.Native.API;
using SSC.Shared.Util;
using SSC.Client.Util;

namespace SSC.Client
{
    public class RaceClient : BaseScript
    {
        public static RaceClient Instance { get; private set; }
        public RaceEventCollection Events { get; private set; }
        public RaceCommandCollection Commands { get; private set; }
        public RaceStateCollection States { get; private set; }
        public RaceWorkerCollection Workers { get; private set; }
        
        public Logger Logger { get; private set; }

        public RaceClient()
        {
            Debug.WriteLine($"[{GetHashCode()}] Race Client has started");
            Instance = this;

            Logger = new Logger(
                (message) => Debug.WriteLine(message),
                ChatHelper.SendMessage
            );

            States = new RaceStateCollection();
            States.SetState(new CommonState());
            States.SetState(new CreatorState());
            States.SetState(new DevToolboxState());

            Commands = new RaceCommandCollection(Logger,
                (name, callback, restricted) => { RegisterCommand(name, callback, restricted); }
            );

            new CreatorCommands();
            new DevToolboxCommands();

            Events = new RaceEventCollection(
                EventHandlers.Add, TriggerEvent, TriggerServerEvent    
            );

            new NotificationEvents();
            new RaceEvents();

            Workers = new RaceWorkerCollection(
                (task) => Tick += task,
                (time) => Delay(time)
            );

            Workers.PushWorker(new CommonWorker());
            Workers.PushWorker(new DevToolboxWorker());
            Workers.PushWorker(new CreatorWorker());
        }

        //[Tick]
        //private async Task OnClientUpdate()
        //{
        //    if (CurrentRace == null)
        //    {
        //        await Task.FromResult(0);
        //        return;
        //    }

        //    if (IsInCreator)
        //    {
        //        await CurrentRace.CreatorUpdate();
        //    }
        //    else
        //    {
        //    }
        //    await Task.FromResult(0);
        //}

        //[Command("rc_create")]
        //public void CommandCreateRace(int source, List<object> args, string raw)
        //{
        //    if (CurrentRace != null)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Race is already created!", 255, 0, 0);
        //        return;
        //    }

        //    if (args.Count < 1)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Please specify a track name.", 255, 0, 0);
        //        return;
        //    }

        //    string raceName = (string)args[0];

        //    CurrentRace = new Race.Race(raceName, true);
        //    ChatHelper.SendMessage(nameof(RaceClient), "A new race has been created!", 0, 255, 0);
        //}

        //[Command("rc_cp")]
        //public void CommandAddCheckpoint()
        //{
        //    if (CurrentRace == null)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Cannot add checkpoint if race doesn't exist.", 255, 0, 0);
        //        return;
        //    }

        //    Vector3 position = LocalPlayer.Character.Position;
        //    CurrentRace.AddCheckpoint(new RaceCheckpoint(position));
        //}

        //[Command("rc_sp")]
        //public void CommandAddStartPoint()
        //{
        //    if (CurrentRace == null)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Cannot add start if race doesn't exist.", 255, 0, 0);
        //        return;
        //    }

        //    Vector3 position = LocalPlayer.Character.Position;
        //    float heading = LocalPlayer.Character.Heading;

        //    CurrentRace.AddStart(new RaceStart(position, heading));
        //}

        //[Command("rc_save")]
        //public void CommandSaveRace()
        //{
        //    if (CurrentRace == null)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Cannot save track if race hasn't been created.", 255, 0, 0);
        //        return;
        //    }

        //    CurrentRace.Save();
        //}

        //[Command("rc_clear")]
        //public void CommandClearRace()
        //{
        //    CurrentRace = null;
        //}

        //[Command("host")]
        //public void CommandHostRace(int source, List<object> args, string raw)
        //{
        //    if (args.Count < 3)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Error, missing parameters, example: /race <track> <car> <laps>", 255, 0, 0);
        //        return;
        //    }

        //    string track = (string)args[0];
        //    string car = (string)args[1];
        //    int laps = int.Parse(args[2].ToString());

        //    TriggerServerEvent("ssc-racing::race::RaceHosted", track, car, laps);
        //}

        //[Command("join")]
        //public void CommandJoinRace(int source, List<object> args, string raw)
        //{
        //    if (args.Count < 1)
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Error, missing parameters, example: /join <id>", 255, 0, 0);
        //        return;
        //    }

        //    string code = (string)args[0];

        //    if (string.IsNullOrEmpty(code))
        //    {
        //        ChatHelper.SendMessage(nameof(RaceClient), "Error, the join code is invalid!", 255, 0, 0);
        //        return;
        //    }

        //    TriggerServerEvent("ssc-racing::race::RaceJoin", code);
        //}
    }
}
