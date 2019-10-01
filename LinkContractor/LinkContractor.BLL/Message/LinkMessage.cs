using System;

namespace LinkContractor.BLL.Message
{
    public class LinkMessage : IMessage
    {
        private LinkMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
        public bool IsLink => true;

        public static bool IsUrl(string message)
        {
            return true; // todo: fix it!
            throw new NotImplementedException();
        }

        public static IMessage TryCreateLinkMessage(string message)
        {
            if (IsUrl(message)) return new LinkMessage(message);

            return new TextMassage(message);
        }

        public static IMessage TryCreateLinkMessageIfNeeded(string message, bool isLink)
        {
            return isLink ? TryCreateLinkMessage(message) : new TextMassage(message);
        }
    }
}