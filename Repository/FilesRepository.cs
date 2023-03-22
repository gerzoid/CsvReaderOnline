using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class FilesRepository : GenericRepository<Files>, IFilesRepository
    {
        public FilesRepository(DbContext context) : base(context)
        {
        }
    }
}
