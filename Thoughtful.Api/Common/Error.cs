namespace Thoughtful.Api.Common
{
    public class Error
    {
        public Error(string message, string code)
        {
            Message = message;
            Code = code;
        }
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
        public string Code { get; set; }
    }
}
