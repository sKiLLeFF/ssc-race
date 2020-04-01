using CitizenFX.Core;

using SSC.Client.States;
using SSC.Client.Util;
using SSC.Shared.Wrappers;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace SSC.Client.Worker
{
    public class DevToolboxWorker : RaceWorker
    {
        public DevToolboxWorker()
        {
            DevToolboxState state = RaceClient.Instance.States.GetState<DevToolboxState>();
            state.CarModel.Observe((model) => GameTask(async () => await OnSpawnVehicle(model)));
        }

        private async Task OnSpawnVehicle(string modelName)
        {
            //TODO: Check if the model is actually a valid model.
            Model vehModel = new Model(modelName);
            bool modelLoaded = await vehModel.Request(1000);

            if (!modelLoaded)
            {
                ChatHelper.SendMessage("DevToolbox", $"Failed to load model {modelName}", 255, 0, 0);
                return;
            }

            Player player = new Player(PlayerId());

            Vector3 vehPosition = player.Character.Position;
            float vehHeading = player.Character.Heading;

            Vehicle vehToSpawn = await World.CreateVehicle(vehModel, vehPosition, vehHeading);
            Debug.WriteLine(vehToSpawn.Handle.ToString());
            player.Character.SetIntoVehicle(vehToSpawn, VehicleSeat.Driver);

            ChatHelper.SendMessage("DevToolbox", "Successfully spawned vehicle", 0, 255, 0);

            DevToolboxState state = RaceClient.Instance.States.GetState<DevToolboxState>();
            state.CarModel.Unset();
        }
    }
}
