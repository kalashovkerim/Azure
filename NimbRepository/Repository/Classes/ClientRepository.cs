using NimbRepository.DbContexts;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Storekeeper;
using NimbRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Classes
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private NimbDbContext _context;

        public ClientRepository(NimbDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client> FindById(int id)
        {
            if(id > 0)
            {
                return _context.Clients.Find(id)!;
            }
            else
            {
                return _context.Clients.FirstOrDefault();
            }
        }

        public void Update(Client? obj)
        {
            var objFromDb = _context.Clients.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {

                    objFromDb.FirstName = obj.FirstName;
                    objFromDb.LastName = obj.LastName;
                    objFromDb.Number = obj.Number;
                    objFromDb.PatronymicName = obj.PatronymicName;
                    objFromDb.EmailAddress = obj.EmailAddress;

                }
            }
        }

    }
}
