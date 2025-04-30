namespace Thoughtful.Api.Common
{
    public class FileManager
    {

        public static byte[] ConvertBase6(string filePath, string filename)
        {
            string imagePath = Path.Combine(filePath, filename);
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return imageBytes;
        }

        //public static byte[] ReadImageFromPath(string filePath, string filename)
        //{
        //    string imagePath = Path.Combine(filePath, filename);
        //    if (!File.Exists(imagePath))
        //        return null; // Or throw an exception / return empty array depending on your needs

        //    return File.ReadAllBytes(imagePath);
        //}
    }
}
