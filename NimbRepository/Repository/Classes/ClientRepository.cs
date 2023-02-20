using NimbRepository.DbContexts;
using NimbRepository.Model.Seller;
using NimbRepository.Repository.Interfaces;

namespace NimbRepository.Repository.Classes
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private NimbDataBaseContext _context;

        public ClientRepository(NimbDataBaseContext context) : base(context)
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
