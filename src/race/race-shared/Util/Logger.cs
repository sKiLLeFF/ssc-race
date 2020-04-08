using System;

namespace SSC.Shared.Util
{
    public class Logger
    {
        public delegate void LoggerConsoleOuput(string message);
        public delegate void LoggerChatOutput(string sender, string message, int red, int blue, int green);

        private LoggerConsoleOuput _consoleOutput;
        private LoggerChatOutput _chatOutput;

        public Logger(LoggerConsoleOuput consoleOutput, LoggerChatOutput chatOutput)
        {
            _consoleOutput = consoleOutput;
            _chatOutput = chatOutput;
        }

        public void Log(string message)
        {
            _consoleOutput?.Invoke(message);
        }

        public void LogToChat(string sender, string message, int r = 255, int g = 255, int b = 255)
        {
            _chatOutput?.Invoke(sender, message, r, g, b);
        }
    }
}
