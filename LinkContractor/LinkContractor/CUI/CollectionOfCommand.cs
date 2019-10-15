using System.Collections.Generic;
using LinkContractor.BLL;
using LinkContractor.CUI.MyConsole;

namespace LinkContractor.CUI
{
    public class CollectionOfCommand : BlConsoleCommand
    {
        public CollectionOfCommand(IConsole console, IBlMain blMain) : base(console, blMain)
        {
            var commands = new List<ConsoleCommand>
            {
                new ClickCommand(console, blMain),
                new AddRecordCommand(console, blMain)
            };
            Commands = commands.AsReadOnly();
        }

        private IReadOnlyList<ConsoleCommand> Commands { get; }

        public override string Name => string.Empty;

        protected override void DoWork(IConsole console)
        {
            while (true)
            {
                console.WriteLine("choose command:");
                for (var i = 0; i < Commands.Count; i++)
                {
                    var command = Commands[i];
                    console.WriteLine($"{i} -> {command.Name}");
                }

                console.WriteLine($"{Commands.Count} -> exit");

                var choise = Get((ref int value) =>
                {
                    if (value > Commands.Count)
                    {
                        console.WriteLine("such command is not available!", ConsoleMessageLevel.Warning);
                        return false;
                    }

                    if (value < 0)
                    {
                        console.WriteLine("number ha to be more than 0", ConsoleMessageLevel.Warning);
                        return false;
                    }

                    return true;
                });
                if (choise == Commands.Count) return;
                console.WriteLine();
                console.WriteLine($"running command '{Commands[choise].Name}':", ConsoleMessageLevel.Info);
                console.WriteLine();
                Commands[choise].DoWork();
                console.WriteLine();
            }
        }
    }
}