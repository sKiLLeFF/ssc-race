using CitizenFX.Core;

namespace SSC.Client.Data
{
    public abstract class RaceComponent
    {
        protected Vector3 position;
        protected Vector3 rotation;

        public RaceComponent()
        {
            position = Vector3.Zero;
            rotation = Vector3.Zero;
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            this.rotation = rotation;
        }

        public abstract void OnDraw();
    }
}
