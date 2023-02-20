using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Repository.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        public Task<Supplier> FindById(int id);
        public void Update(Supplier? obj); 
    }
}