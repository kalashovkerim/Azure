using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Repository.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        public Task<Good> FindById(int id);
        public void Update(Good? obj); 
    }
}
