namespace Contracts.Repository
{
    public interface IRepositoryManager
    {
        IFilesRepository FilesRepository { get; }
        IUsersRepository UsersRepository { get; }
        void Save();
    }
}
