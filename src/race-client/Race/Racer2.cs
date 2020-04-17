namespace SSC.Client.Race
{
    public class Racer2
    {
        public int RacerServerId = -1;

        public bool IsRacerAI
        {
            get
            {
                return RacerServerId == -1;
            }
        }

        public Racer2(int serverId)
        {
            RacerServerId = serverId;
        }
    }
}
