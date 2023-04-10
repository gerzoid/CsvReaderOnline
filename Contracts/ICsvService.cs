using Entities.Answer;
using Entities.Models;
using Entities.Other;
using Entities.Query;

namespace Contracts
{
    public interface ICsvService
    {
        public Task<CsvFileInfo> UploadFile(FileModel file);
        public CsvFileInfo OpenFile(string path, bool hasHeader = false);
        public void CheckCountFileByUserAndDelete(string userId);
        public Users GetOrCreateUser(string userId);
        //public List<Dictionary<string, object>> GetData(QueryGetData queryData);
        public AnswerGetData GetData(QueryGetData queryData);
    }
}