using System.ComponentModel.DataAnnotations;

namespace Thoughtful.Api.Common
{
    public class AppSettings
    {
        public static string UploadFilePath { get; set; }
        public static string ProfilePhotosPath { get; set; }
        public static EmailModel EmailParameters {  get; set; }

        public static void Initiate(string uploadFilePath, string profilePhotoPath,EmailModel emailParameters)
        {
            UploadFilePath = uploadFilePath;
            ProfilePhotosPath = profilePhotoPath;
            EmailParameters = emailParameters;
        }
        public class EmailModel
        {
            public string fromEmail { get; set; }
            public string fromPassword { get; set; }
        }
    }
}
