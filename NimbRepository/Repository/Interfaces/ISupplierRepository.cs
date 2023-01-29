using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Repository.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        public Supplier? FindById(int id);
        public void Update(Supplier? obj);
    }
}