namespace SSC.Shared.Util
{
    public static class Mathman
    {
        public static int Clamp(int input, int min, int max)
        {
            if (input > max) return max;
            else if (input < min) return min;
            else return input;
        }

        public static uint Clamp(uint input, uint min, uint max)
        {
            if (input > max) return max;
            else if (input < min) return min;
            else return input;
        }

        public static float Clamp(float input, float min, float max)
        {
            if (input > max) return max;
            else if (input < min) return min;
            else return input;
        }
    }
}
