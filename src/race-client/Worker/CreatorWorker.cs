using System.Drawing;
using System.Threading.Tasks;

using CitizenFX.Core;

using SSC.Shared.Wrappers;
using SSC.Shared.Util;

using SSC.Client.States;
using SSC.Client.Util;

using static CitizenFX.Core.Native.API;

namespace SSC.Client.Worker
{
    public class CreatorWorker : RaceWorker
    {
        private CommonState commonState;
        private CreatorState creatorState;

        private uint checkpointState = 0;
        private uint checkpointMaxStates = 5;
        private string[] checpointStateNames = new string[] { "n.a.", "rotation", "size", "Icon", "offset", "commit" };
        private MarkerType[] checkpointIcons = new MarkerType[] { MarkerType.ChevronUpx1, MarkerType.ChevronUpx2, MarkerType.ChevronUpx3, MarkerType.ReplayIcon };

        private Vector3 checkpointLastPosition = Vector3.Zero;
        private Vector3 checkpointOffset = Vector3.Zero;
        private float checkpointRotation = 0.0f;
        private float checkpointSize = 1.0f;
        private int checkpointIconCount = 0;
        private MarkerType checkpointIcon = MarkerType.ChevronUpx1;

        public override Task OnRefreshState()
        {
            RaceStateCollection RSC = RaceClient.Instance.States;
            commonState = RSC.GetState<CommonState>();
            creatorState = RSC.GetState<CreatorState>();

            creatorState.PlaceCheckpoint.Observe(
                (state) => checkpointState = state == true ? 1u : 0u
            );

            return Wait(100);
        }

        public override Task OnGameRenderTick()
        {
            // We are not in creator mode.
            if (commonState == null || !commonState.IsInCreator.Get()) return Task.FromResult(0);

            // Player is not known yet.
            if (commonState.LocalPlayer.Get().Handle < 0) return Task.FromResult(0);

            //Check controls if we are in checkpoint edit mode.
            if (checkpointState > 0)
            {
                OnCheckpointPlacementControls();
            }

            switch (checkpointState)
            {
                case 0:
                    OnCheckpointPlacementPreview();
                    break;
                case 1:
                    OnCheckpointPlacementRotation();
                    break;
                case 2:
                    OnCheckpointPlacementSize();
                    break;
                case 3:
                    OnCheckpointPlacementIcon();
                    break;
                case 4:
                    OnCheckpointPlacementOffset();
                    break;
                case 5:
                    OnCheckpointPlacementCommit();
                    break;
            }

            return Task.FromResult(0);
        }

        private void OnCheckpointPlacementControls()
        {
            //152 -> Q, LB.
            if (IsControlJustPressed(0, 152))
            {
                checkpointState = Mathman.Clamp(++checkpointState, 0, checkpointMaxStates);
                ChatHelper.SendMessage("Creator", $"Editing {checpointStateNames[checkpointState]}", 0, 255, 0);
            }
            //86 -> E, L3
            else if (IsControlJustPressed(0, 86))
            {
                checkpointState = Mathman.Clamp(--checkpointState, 0, checkpointMaxStates);
                ChatHelper.SendMessage("Creator", $"Editing checkpoint {checpointStateNames[checkpointState]}", 0, 255, 0);
            }
        }

        private Task OnCheckpointPlacementPreview()
        {
            // Don't render the checkpoint preview.
            if (!creatorState.OptionPreview)
            {
                return Task.FromResult(0);
            }

            // Calculate the position of the marker.
            float checkpointDistance = 10.0f;

            if (creatorState.ZFix)
            {
                GameTask(async () =>
                {
                    Vector3 markerPosition = commonState.LocalPlayerPosition + (commonState.LocalPlayerDirections[0] * checkpointDistance);
                    checkpointLastPosition = await GroundHelper.PositionOnGround(markerPosition);
                });
            }
            else
            {
                checkpointLastPosition = commonState.LocalPlayerPosition + (commonState.LocalPlayerDirections[0] * checkpointDistance);
            }

            RenderCheckpoint();
            return Task.FromResult(0);
        }

        private void RenderCheckpoint()
        {
            //Draw the circle marker.
            World.DrawMarker(
                 MarkerType.VerticalCylinder,
                 checkpointLastPosition + checkpointOffset,
                 Vector3.Up, Vector3.Zero, Vector3.One * (5.0f * checkpointSize),
                 Color.FromArgb(80, 120, 170, 255),
                 false,false, false,
                 null, null, false
             );

            //Draw the inner marker.
            float innerMarkerSize = 3.0f;
            World.DrawMarker(
                checkpointIcon,
                (checkpointLastPosition + checkpointOffset) + Vector3.ForwardLH * innerMarkerSize,
                Vector3.Zero,
                new Vector3(checkpointRotation, 90.0f, 180.0f), 
                Vector3.One * innerMarkerSize,
                Color.FromArgb(100, 50, 130, 255),
                false, false, false,
                null, null, false
            );
        }

        private Task OnCheckpointPlacementRotation()
        {
            if (IsControlJustPressed(0, 174))
            {
                checkpointRotation -= 10.001f;
            }
            else if(IsControlJustPressed(0, 175))
            {
                checkpointRotation += 10.001f;
            }

            RenderCheckpoint();
            return Task.FromResult(0);
        }

        private Task OnCheckpointPlacementSize()
        {
            if (IsControlJustPressed(0, 172))
            {
                checkpointSize += 0.25f;
            }
            else if (IsControlJustPressed(0, 173))
            {
                checkpointSize -= 0.25f;
            }

            checkpointSize = Mathman.Clamp(checkpointSize, 1.0f, 20.0f);

            RenderCheckpoint();
            return Task.FromResult(0);
        }

        private Task OnCheckpointPlacementIcon()
        {
            if (IsControlJustPressed(0, 172))
            {
                checkpointIconCount++;
            }
            else if (IsControlJustPressed(0, 173))
            {
                checkpointIconCount--;
            }

            checkpointIconCount = Mathman.Clamp(checkpointIconCount, 0, checkpointIcons.Length - 1);
            checkpointIcon = checkpointIcons[checkpointIconCount];

            RenderCheckpoint();
            return Task.FromResult(0);
        }

        private Task OnCheckpointPlacementOffset()
        {
            if (IsControlJustPressed(0, 172))
            {
                checkpointOffset += commonState.LocalPlayerDirections[0] * 0.2f;
            }
            else if (IsControlJustPressed(0, 173))
            {
                checkpointOffset += commonState.LocalPlayerDirections[2] * 0.2f;
            }
            else if (IsControlJustPressed(0, 174))
            {
                checkpointOffset += commonState.LocalPlayerDirections[3] * 0.2f;
            }
            else if (IsControlJustPressed(0, 175))
            {
                checkpointOffset += commonState.LocalPlayerDirections[1] * 0.2f;
            }

            RenderCheckpoint();
            return Task.FromResult(0);
        }

        private Task OnCheckpointPlacementCommit()
        {
            //TODO(bma): Store the checkpoint data somewhere.
            checkpointState = 0;
            checkpointLastPosition = Vector3.Zero;
            checkpointOffset = Vector3.Zero;
            checkpointRotation = 0.0f;
            checkpointSize = 1.0f;
            checkpointIconCount = 0;
            checkpointIcon = MarkerType.ChevronUpx1;

            creatorState.PlaceCheckpoint.Unset();
            return Task.FromResult(0);
        }
    }
}
