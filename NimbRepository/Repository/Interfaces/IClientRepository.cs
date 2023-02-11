using NimbRepository.Model.Seller;
using NimbRepository.Model.Storekeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        public Task<Client> FindById(int id);
        public void Update(Client? obj); 
    }
}
