using NimbRepository.Model.Admin;

namespace NimbRepository.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> FindById(int id);
        public void Update(User? obj); 
    }
}
