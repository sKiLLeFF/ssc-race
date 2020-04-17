using SSC.Client.Data;
using SSC.Shared.Util;

namespace SSC.Client.States
{
    public class CreatorState
    {
        public Observable<bool> PlaceCheckpoint = new Observable<bool>(false);

        public bool ZFix = true;
        public bool OptionPreview = true;

        public RaceCheckpoint PreviewCheckpoint = new RaceCheckpoint();
    }
}
