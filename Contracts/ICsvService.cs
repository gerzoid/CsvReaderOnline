using Entities.Other;

namespace Contracts
{
    public interface ICsvService
    {
        public CsvFileInfo OpenFile(string path, bool hasHeader = false);
    }
}