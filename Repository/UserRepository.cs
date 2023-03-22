using Contracts.Repository;
using Entities.Models;

namespace Repository
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
