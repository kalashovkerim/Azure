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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private NimbDbContext _context;

        public CompanyRepository(NimbDbContext context) : base(context)
        {
            _context = context;
        }

        public Company FindById(int id)
        {
            if(id > 0)
            {
                return _context.Companies.Find(id);
            }
            else
            {
                return _context.Companies.FirstOrDefault();
            }
        }

        public void Update(Company? obj)
        {
            var objFromDb = _context.Companies.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {
                    objFromDb.Name = obj.Name;
                    objFromDb.Address = obj.Address;
                    objFromDb.EmailAddress = obj.EmailAddress;
                    objFromDb.Number = obj.Number;
                }
            }
        }
    }
}

