using System;

using CitizenFX.Core;

using static CitizenFX.Core.Native.API;

namespace SSC.Server
{
    public class ChatEvents 
    {
        public ChatEvents()
        {
            var events = RaceServer.Instance.Events;
            events.RegisterRawEvent("chatMessage", new Action<int, string, string>(OnChatMessage));
        }

        public void OnChatMessage(int source, string name, string message)
        {
            if (!string.IsNullOrEmpty(message) && message[0] == '/')
            {
                CancelEvent();
                //Debug.WriteLine($"Cancelled message: ^1{name} -^2{message}^7");
                //TODO(bma): Log filtered messages.
            }
        }
    }
}
