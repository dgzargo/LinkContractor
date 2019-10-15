using System;

namespace LinkContractor.CUI.MyConsole
{
    public interface IConsole
    {
        void WriteLine(string message = null, ConsoleMessageLevel level = ConsoleMessageLevel.Default);
        string ReadLine();
    }

    public class ConsoleProxy : IConsole
    {
        public ConsoleProxy(ConsoleTheme? theme = null)
        {
            Theme = theme ?? ConsoleTheme.CreateDefault();
        }

        public ConsoleTheme Theme { get; }

        public void WriteLine(string message = null, ConsoleMessageLevel level = ConsoleMessageLevel.Default)
        {
            SetMessageLevel(level);
            Console.WriteLine(message);
            SetMessageLevel(ConsoleMessageLevel.Default);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        private void SetProfile(ConsoleColorProfile profile)
        {
            Console.ForegroundColor = profile.ForegroundColor;
            Console.BackgroundColor = profile.BackgroundColor;
        }

        protected void SetMessageLevel(ConsoleMessageLevel level)
        {
            SetProfile(Theme[level]);
        }

        [Obsolete]
        protected void DoSomething(Action action, ConsoleMessageLevel level)
        {
            SetMessageLevel(level);
            try
            {
                action();
            }
            finally
            {
                SetMessageLevel(ConsoleMessageLevel.Warning);
            }

            SetMessageLevel(ConsoleMessageLevel.Default);
        }
    }
}