using System.Drawing;

using CitizenFX.Core;

namespace SSC.Client.Data
{
    public class RaceCheckpoint : RaceComponent, IStaticAdapter<RaceCheckpointStatic>
    {
        private float checkpointSize = 1.0f;
        private float checkpointIconSize = 1.0f;
        private float checkpointIconHeight = 3.0f;
        private MarkerType checkpointIcon = MarkerType.ChevronUpx1;

        public void SetCheckpointSize(float size)
        {
            checkpointSize = size;
        }

        public void SetCheckpointIcon(MarkerType iconType)
        {
            checkpointIcon = iconType;
        }

        public void SetCheckpointIconSize(float iconSize)
        {
            checkpointIconSize = iconSize;
        }

        public void SetCheckpointIconHeight(float iconHeight)
        {
            checkpointIconHeight = iconHeight;
        }

        public override void OnDraw()
        {
            //Draw the circle marker.
            World.DrawMarker(
                 MarkerType.VerticalCylinder,
                 position,
                 Vector3.Up, 
                 Vector3.Zero, 
                 Vector3.One * checkpointSize,
                 Color.FromArgb(80, 120, 170, 255),
                 false, false, false,
                 null, null, false
             );

            //Draw the inner marker.
            World.DrawMarker(
                checkpointIcon,
                position + Vector3.ForwardLH * (checkpointIconHeight - (checkpointIconSize /  2)),
                Vector3.Zero,
                new Vector3(rotation.X, 90.0f, 180.0f),
                Vector3.One * checkpointIconSize,
                Color.FromArgb(100, 50, 130, 255),
                false, false, false,
                null, null, false
            ) ;
        }

        public RaceCheckpointStatic ToStaticData()
        {
            return new RaceCheckpointStatic(
                this.position, this.rotation,
                checkpointSize, checkpointIconSize,
                checkpointIconHeight, checkpointIcon
            );
        }
    }
}
