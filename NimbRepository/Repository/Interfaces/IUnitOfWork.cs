namespace NimbRepository.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public ISupplierRepository Supplier { get; }
        public IGoodRepository Good { get;}
        public IClientRepository Client { get; }
        void Save();
    }
}
