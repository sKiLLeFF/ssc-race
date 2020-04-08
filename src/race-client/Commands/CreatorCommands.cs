using System;

using SSC.Client.Util;
using SSC.Client.States;
using SSC.Shared.Wrappers;

namespace SSC.Client.Commands
{
    public class CreatorCommands
    {
        private RaceClient RC => RaceClient.Instance;

        public CreatorCommands()
        {
            RaceCommandDefinition cmdCreatorStart = RC.Commands.Create()
                .AddCommandName("creator", "start")
                .AddSuccessCallback(new Action<string>(OnCreatorStart))
                .AddFailedCallback(new Action<string, string>(OnCommandFailed))
                .AddParam<string>("track_name", new RaceCommandCheckArgs { Min = 3, Max = 32 });

            RaceCommandDefinition cmdCreatorCheckpoint = RC.Commands.Create()
                .AddCommandName("creator", "cp")
                .AddSuccessCallback(new Action(OnCreatorAddCheckpoint))
                .AddFailedCallback(new Action<string, string>(OnCommandFailed));

            RaceCommandDefinition cmdCreatorOption = RC.Commands.Create()
                .AddCommandName("creator", "option")
                .AddSuccessCallback(new Action<string, bool>(OnCreatorOption))
                .AddFailedCallback(new Action<string, string>(OnCommandFailed))
                .AddParam<string>("option", new RaceCommandCheckArgs())
                .AddParam<bool>("optionToggle", new RaceCommandCheckArgs());

            RC.Commands.Register(cmdCreatorStart);
            RC.Commands.Register(cmdCreatorCheckpoint);
            RC.Commands.Register(cmdCreatorOption);
        }

        public void OnCreatorStart(string trackName)
        {
            CommonState common = RC.States.GetState<CommonState>();

            if (!common.IsInRace.Get() && !common.IsInCreator.Get())
            {
                common.IsInCreator.Set(true);
                ChatHelper.SendMessage("Creator", $"Creator mode enabled, now creating track {trackName}", 0, 255, 0);
            }
        }

        public void OnCreatorOption(string optionName, bool state)
        {
            ChatHelper.SendMessage("Creator", $"{optionName}: {state}", 255, 0, 0);


            CreatorState creator = RC.States.GetState<CreatorState>();

            if (string.Compare(optionName, "zfix", true) == 0)
            {
                creator.ZFix = state;
            }
            else if (string.Compare(optionName, "preview") == 0)
            {
                creator.OptionPreview = state;
            }
            else
            {
                ChatHelper.SendMessage("Creator", $"Option {optionName} was not found", 255, 0, 0);
            }
        }

        public void OnCreatorAddCheckpoint()
        {
            CommonState common = RC.States.GetState<CommonState>();
            CreatorState creator = RC.States.GetState<CreatorState>();

            if (!common.IsInCreator.Get())
            {
                ChatHelper.SendMessage("Creator", "You must be in creator mode to add checkpoints.", 255, 150, 0);
                return;
            }

            if (creator.PlaceCheckpoint.Get())
            {
                ChatHelper.SendMessage("Creator", "Already placing a new checkpoint...", 255, 0, 0);
                return;
            }

            creator.PlaceCheckpoint.Set(true);
        }

        public void OnCommandFailed(string reason, string usage)
        {
            ChatHelper.SendMessage("Creator", $"Command failed: {reason}", 255, 0, 0);
            ChatHelper.SendMessage("Creator", $"Usage: {usage}", 255, 0, 0);
        }
    }
}
