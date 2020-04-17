using CitizenFX.Core;

namespace SSC.Client.Data
{
    public class RaceCheckpointStatic
    {
        public float PositionX = 0.0f;
        public float PositionY = 0.0f;
        public float PositionZ = 0.0f;

        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float RotationZ = 0.0f;

        public float CheckpointSize = 0.0f;
        public float CheckpointIconSize = 0.0f;
        public float CheckpointIconHeight = 0.0f;
        public int CheckpointIcon = 0;

        public RaceCheckpointStatic(
            Vector3 position, Vector3 rotation, 
            float cpSize, float cpIconSize, float cpIconHeight, 
            MarkerType cpIcon)
        {
            PositionX = position.X;
            PositionY = position.Y;
            PositionZ = position.Z;

            RotationX = rotation.X;
            RotationY = rotation.Y;
            RotationZ = rotation.Z;

            CheckpointSize = cpSize;
            CheckpointIconSize = cpIconSize;
            CheckpointIconHeight = cpIconHeight;
            CheckpointIcon = (int)cpIcon;
        }
    }
}
