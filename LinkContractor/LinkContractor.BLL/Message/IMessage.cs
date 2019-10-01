namespace LinkContractor.BLL.Message
{
    public interface IMessage
    {
        string Message { get; }
        bool IsLink { get; }
    }
}