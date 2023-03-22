namespace Utils
{
    public static class Helper
    {        
        public const int COUNT_MAX_UPLOAD_FILES = 5;

        public static string UploadFolder ="wwwwroot/upload";
        public static string GetUploadPathFolder(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), UploadFolder, fileName);
        }

    }
}