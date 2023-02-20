using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;

namespace NimbRepository.Repository.Classes
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private NimbDataBaseContext _context;

        public UserRepository(NimbDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> FindById(int id)
        {
            if (id > 0)
            {
                return _context.Users.Find(id)!;
            }
            else
            {
                return _context.Users.FirstOrDefault();
            }
        }

        public void Update(User? obj)
        {
            var objFromDb = _context.Users!.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {

                    objFromDb.FirstName = obj.FirstName;
                    objFromDb.LastName = obj.LastName;
                    objFromDb.Number = obj.Number;
                    objFromDb.Address = obj.Address;
                    objFromDb.EmailAddress = obj.EmailAddress;
                    objFromDb.PatronymicName = obj.PatronymicName;
                    objFromDb.Position = obj.Position;
                }
            }
        }

    }
}
