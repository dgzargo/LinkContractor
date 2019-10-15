using System;

namespace LinkContractor.CUI.MyConsole
{
    public struct ConsoleTheme
    {
        private ConsoleColorProfile Default { get; set; }
        private ConsoleColorProfile Warning { get; set; }
        private ConsoleColorProfile Info { get; set; }

        private ConsoleColorProfile Error { get; set; }

        public ConsoleColorProfile this[ConsoleMessageLevel level]
        {
            get => level switch
            {
                ConsoleMessageLevel.Default => Default,
                ConsoleMessageLevel.Warning => Warning,
                ConsoleMessageLevel.Info => Info,
                ConsoleMessageLevel.Error => Error,
                _ => throw new ArgumentException(),
            };
            private set
            {
                switch (level)
                {
                    case ConsoleMessageLevel.Default:
                    {
                        Default = value;
                        break;
                    }
                    case ConsoleMessageLevel.Warning:
                    {
                        Warning = value;
                        break;
                    }
                    case ConsoleMessageLevel.Info:
                    {
                        Info = value;
                        break;
                    }
                    case ConsoleMessageLevel.Error:
                    {
                        Error = value;
                        break;
                    }
                    default: throw new ArgumentException();
                }
            }
        }

        public ConsoleTheme With(ConsoleMessageLevel level, ConsoleColorProfile profile)
        {
            var @new = this;
            @new[level] = profile;
            return @new;
        }

        public static ConsoleTheme CreateDefault()
        {
            ConsoleColorProfile Set(ConsoleColor? fg = null, ConsoleColor? bg = null)
            {
                return ConsoleColorProfile.Set(fg, bg);
            }

            return new ConsoleTheme()
                .With(ConsoleMessageLevel.Warning, Set(ConsoleColor.Red, ConsoleColor.DarkGray))
                .With(ConsoleMessageLevel.Info, Set(ConsoleColor.Green, ConsoleColor.Gray));
        }
    }

    public struct ConsoleColorProfile
    {
        private ConsoleColor? _foregroundColor;
        private ConsoleColor? _backgroundColor;

        public ConsoleColor ForegroundColor
        {
            get => _foregroundColor ?? ConsoleColor.Gray;
            private set => _foregroundColor = value;
        }

        public ConsoleColor BackgroundColor
        {
            get => _backgroundColor ?? ConsoleColor.Black;
            private set => _backgroundColor = value;
        }

        public static ConsoleColorProfile DefaultProfile => new ConsoleColorProfile();

        public static ConsoleColorProfile Set(ConsoleColor? fg, ConsoleColor? bg = null)
        {
            var pair = new ConsoleColorProfile();
            if (fg.HasValue) pair.ForegroundColor = fg.Value;
            if (bg.HasValue) pair.BackgroundColor = bg.Value;
            return pair;
        }
    }

    public enum ConsoleMessageLevel
    {
        Default,
        Warning,
        Error,
        Info
    }
}