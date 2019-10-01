namespace LinkContractor.BLL.Message
{
    public class TextMassage : IMessage
    {
        public TextMassage(string message)
        {
            Message = message;
        }

        public string Message { get; }
        public bool IsLink => false;
    }
}