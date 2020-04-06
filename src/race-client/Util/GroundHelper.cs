using CitizenFX.Core;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace SSC.Client.Util
{
    public static class GroundHelper
    {
        public static async Task<Vector3> PositionOnGround(Vector3 inPosition)
        {
            int startTime = GetGameTimer();
            Vector3 groundPosition = Vector3.Zero;
            bool searching = true;

            while (searching)
            {
                RequestCollisionAtCoord(inPosition.X, inPosition.Y, 0.0f);

                float groundZ = 0.0f;
                bool hasFoundGround = GetGroundZFor_3dCoord(inPosition.X, inPosition.Y, 1000.0f, ref groundZ, false);

                if (!hasFoundGround)
                {
                    if ((GetGameTimer() - startTime) > 5000)
                    {
                        ChatHelper.SendMessage(nameof(GroundHelper), $"Failed to find ground for position X: {inPosition.X}, Y: {inPosition.Y}, Z: {inPosition.Z}", 255, 0, 0);
                        searching = false;
                    }
                }
                else
                {
                    groundPosition = new Vector3(inPosition.X, inPosition.Y, groundZ);
                    searching = false;
                }

                await BaseScript.Delay(20);
            }

            return groundPosition;
        }
    }
}
