namespace Utils
{
    public static class Helper
    {        
        public static string UploadFolder ="wwwwroot/upload";
        public static string GetUploadPathFolder(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), UploadFolder, fileName);
        }

    }
}