using NimbRepository.DbContexts;
using NimbRepository.Repository.Interfaces;

namespace NimbRepository.Repository.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IGoodRepository Good { get; private set; }
        public IClientRepository Client { get; private set; }


        private NimbDataBaseContext _context;
        public UnitOfWork(NimbDataBaseContext context)
        {
            _context = context;

            User = new UserRepository(_context);
            Supplier = new SupplierRepository(_context);
            Good = new GoodRepository(_context);
            Client = new ClientRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
