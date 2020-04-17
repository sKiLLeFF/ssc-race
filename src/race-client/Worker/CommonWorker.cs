using System.Threading.Tasks;

using CitizenFX.Core;

using SSC.Shared.Wrappers;

using SSC.Client.States;

using static CitizenFX.Core.Native.API;

namespace SSC.Client.Worker
{
    public class CommonWorker : RaceWorker
    {
        public override Task OnGameLogicTick()
        {
            CommonState common = RaceClient.Instance.States.GetState<CommonState>();

            Player player = new Player(PlayerId());
            Ped playerPed = player.Character;
            Vector3 position = playerPed.Position;
            Vector3 rotation = playerPed.Rotation;
            Vector3 forward = playerPed.ForwardVector;
            float heading = playerPed.Heading;

            common.LocalPlayer.Set(player, true);
            common.LocalPlayerPosition = position;
            common.LocalPlayerRotation = rotation;
            common.LocalPlayerHeading = heading;

            //Ordered in clockwise (forward, right, back, left, up, down).
            common.LocalPlayerDirections = new Vector3[]
            {
                playerPed.ForwardVector,
                playerPed.RightVector,
                -playerPed.ForwardVector,
                -playerPed.RightVector,
                playerPed.UpVector,
                -playerPed.UpVector
            };

            return Task.FromResult(0);
        }
    }
}
