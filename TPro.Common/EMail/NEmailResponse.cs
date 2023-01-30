namespace TPro.Common.EMail
{
    public class NEmailResponse
    {
        public enum ResponseStatus
        {
            Success,
            Fail
        }

        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public NEmailResponse(string message, ResponseStatus status = ResponseStatus.Success)
        {
            Message = message;
            Status = status;
        }

        public static NEmailResponse Success()
        {
            return new NEmailResponse("");
        }

        public static NEmailResponse Fail(string msg)
        {
            return new NEmailResponse(msg, ResponseStatus.Fail);
        }
    }
}