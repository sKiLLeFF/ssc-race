using CitizenFX.Core;
using SSC.Shared.Util;

namespace SSC.Client.States
{
    public class CommonState
    {
        public Observable<bool> IsInCreator = new Observable<bool>(false);
        public Observable<bool> IsInRace = new Observable<bool>(false);

        public Observable<Player> LocalPlayer = new Observable<Player>();

        public Vector3 LocalPlayerPosition = Vector3.Zero;
        public Vector3 LocalPlayerRotation = Vector3.Zero;
        public Vector3[] LocalPlayerDirections = new Vector3[6];
        public float LocalPlayerHeading = 0.0f;
    }
}
