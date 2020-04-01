using SSC.Shared.Util;

namespace SSC.Client.States
{
    public class CommonState
    {
        public Observable<bool> IsInCreator = new Observable<bool>(false);
        public Observable<bool> IsInRace = new Observable<bool>(false);
    }
}
