namespace Thoughtful.Api.Common
{
    public class AppSettings
    {
        public static string UploadFilePath { get; set; }

        public static void Initiate(string uploadFilePath)
        {
            UploadFilePath = uploadFilePath;
        }
    }
}
