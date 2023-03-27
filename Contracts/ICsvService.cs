using Entities.Models;
using Entities.Other;

namespace Contracts
{
    public interface ICsvService
    {
        public Task<CsvFileInfo> UploadFile(FileModel file);
        public CsvFileInfo OpenFile(string path, bool hasHeader = false);
        public void CheckCountFileByUserAndDelete(string userId);
        public Users GetOrCreateUser(string userId);
    }
}