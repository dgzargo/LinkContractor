using System;
using System.ComponentModel;
using System.Linq;
using LinkContractor.BLL;
using LinkContractor.CUI.MyConsole;

namespace LinkContractor.CUI
{
    public abstract class ConsoleCommand
    {
        protected ConsoleCommand(string repeatMessage, IConsole console)
        {
            RepeatMessage = repeatMessage;
            Console = console;
        }

        private string RepeatMessage { get; }
        private IConsole Console { get; }
        public abstract string Name { get; }

        protected TType Get<TType>(IsCorrect<TType> isCorrect = null,
            string specificRepeatMessage = null)
        {
            return Get(TryParseOf, isCorrect, specificRepeatMessage);
        }

        private bool TryParseOf<TType>(string s, out TType result)
        {
            if (typeof(TType).Name == typeof(Nullable<>).Name)
                if (s.ToLower() == "null")
                {
                    result = (TType) (object) null;
                    return true;
                }

            var converter = TypeDescriptor.GetConverter(typeof(TType));
            try
            {
                result = (TType) converter.ConvertFromString(s);
            }
            catch (Exception) //ArgumentException, FormatException:
            {
                result = default;
                return false;
            }

            return true;
        }

        protected TResult Get<TResult>(TryParse<TResult> tryParse,
            IsCorrect<TResult> isCorrect = null,
            string specificRepeatMessage = null)
        {
            while (true)
            {
                //Console.WriteLine($@"{message} ({TypeDefinition<TResult>()})");-
                var receivedString = Console.ReadLine();
                var parseSuccessful = tryParse(receivedString, out var result);
                if (parseSuccessful)
                {
                    if (isCorrect is null || isCorrect(ref result)) return result;
                }
                else
                {
                    Console.WriteLine(specificRepeatMessage ?? RepeatMessage, ConsoleMessageLevel.Warning);
                }
            }
        }

        protected string TypeDefinition<TType>()
        {
            var t = typeof(TType);
            var genericDescription = string.Empty;
            if (t.IsGenericType)
                genericDescription =
                    $"<{t.GenericTypeArguments.Select(ts => ts.Name).Aggregate((s1, s2) => $"{s1},{s2}")}>";
            return $"{t.Name}{genericDescription}";
        }

        protected bool GetYesOrNo()
        {
            bool TryParseYesOrNo(string s, out bool result)
            {
                switch (s.ToLower())
                {
                    case "y":
                    case "yes":
                        result = true;
                        return true;
                    case "n":
                    case "no":
                        result = false;
                        return true;
                    default:
                        result = default;
                        return false;
                }
            }

            return Get<bool>(TryParseYesOrNo, specificRepeatMessage: "this is yes or no question!");
        }

        public void DoWork()
        {
            DoWork(Console);
        }

        protected abstract void DoWork(IConsole console);

        protected delegate bool TryParse<TResult>(string s, out TResult result);

        protected delegate bool IsCorrect<TValue>(ref TValue value);
    }

    public abstract class BlConsoleCommand : ConsoleCommand
    {
        protected IBlMain Bl { get; }

        protected BlConsoleCommand(IConsole console, IBlMain blMain) : base("something wrong. try again!", console)
        {
            Bl = blMain;
        }
    }
}