namespace Thoughtful.Api.Common
{
    public class AppSettings
    {
        public static string UploadFilePath { get; set; }
        public static string ProfilePhotosPath { get; set; }

        public static void Initiate(string uploadFilePath, string profilePhotoPath)
        {
            UploadFilePath = uploadFilePath;
            ProfilePhotosPath = profilePhotoPath;
        }
    }
}
