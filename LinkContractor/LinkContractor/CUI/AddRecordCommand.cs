using System;
using LinkContractor.BLL;
using LinkContractor.CUI.MyConsole;

namespace LinkContractor.CUI
{
    public class AddRecordCommand : BlConsoleCommand
    {
        public AddRecordCommand(IConsole console, IBlMain bl) : base(console, bl)
        {
        }

        public override string Name => "Add record";

        protected override void DoWork(IConsole console)
        {
            console.WriteLine("type message or link!");
            var message = Get<string>();
            console.WriteLine("was it a link?");
            var isLink = Get<bool>();
            console.WriteLine("type time limit in days if you want!");
            var timeLimit = Get((ref int? i) =>
            {
                if (i > 14)
                {
                    console.WriteLine("time limit has to be less then 15", ConsoleMessageLevel.Warning);
                    return false;
                }

                if (i <= 0)
                {
                    console.WriteLine("time limit has to be more then 0", ConsoleMessageLevel.Warning);
                    return false;
                }

                return true;
            });
            console.WriteLine("type click limit in days if you want!");
            var clickLimit = Get((ref int? i) =>
            {
                if (i > 0) return true;
                if (i is null) return true;
                console.WriteLine("number of clicks have to be positive number!", ConsoleMessageLevel.Warning);
                return false;
            });
            console.WriteLine("put user guid or ignore it");
            var user = Get<Guid?>();
            console.WriteLine("do you need to create short code? (y/n)");
            var isShortLinkNeeded = GetYesOrNo();

            Console.WriteLine((string) null);
            console.WriteLine();
            var code = Bl.AddRecord(Extension.CreateSavedDataDTO(message, isLink, timeLimit, clickLimit, user),
                isShortLinkNeeded);
            console.WriteLine($"your code is: '{code}'");
        }
    }
}