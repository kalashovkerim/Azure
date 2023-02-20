using NimbRepository.Model.Seller;


namespace NimbRepository.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        public Task<Client> FindById(int id);
        public void Update(Client? obj); 
    }
}
