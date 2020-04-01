using System;

using CitizenFX.Core;

using SSC.Shared.Wrappers;

using SSC.Client.States;
using SSC.Client.Util;

namespace SSC.Client.Commands
{
    public class DevToolboxCommands
    {
        RaceClient RC => RaceClient.Instance;

        public DevToolboxCommands()
        {
            RaceCommandDefinition dvtCar = RC.Commands.Create()
                .AddCommandName("dvt", "car")
                .AddSuccessCallback(new Action<string>(OnCarSpawn))
                .AddFailedCallback(new Action<string, string>(OnCommandFailed))
                .AddParam<string>("carModel", new RaceCommandCheckArgs { Min = 3, Max = 20 });

            RC.Commands.Register(dvtCar);
        }

        public void OnCarSpawn(string model)
        {
            DevToolboxState state = RC.States.GetState<DevToolboxState>();
            state.CarModel.Set(model);
        }

        public void OnCommandFailed(string reason, string usage)
        {
            ChatHelper.SendMessage("DevToolbox", $"Command failed: {reason}", 255, 0, 0);
            ChatHelper.SendMessage("DevToolbox", $"Usage: {usage}", 255, 0, 0);
        }
    }
}
