using LinkContractor.BLL;
using LinkContractor.CUI.MyConsole;

namespace LinkContractor.CUI
{
    public class ClickCommand : BlConsoleCommand
    {
        public ClickCommand(IConsole console, IBlMain bl) : base(console, bl)
        {
        }

        public override string Name => "find record by short code";

        protected override void DoWork(IConsole console)
        {
            console.WriteLine("enter short code:");
            var code = Get((ref string value) =>
            {
                if (!string.IsNullOrWhiteSpace(value)) return true;
                console.WriteLine("code has to be not empty!", ConsoleMessageLevel.Warning);
                return false;
            });
            var message = Bl.Click(code);
            if (message is null)
            {
                console.WriteLine("cant find any record with this short code!", ConsoleMessageLevel.Error);
            }
            else
            {
                console.WriteLine($"your {(message.IsLink ? "link" : "text")} record:");
                console.WriteLine(message.Message);
            }
        }
    }
}