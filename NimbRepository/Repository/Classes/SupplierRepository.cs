using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;
using NimbRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Classes
{
    internal class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private NimbDbContext _context;

        public SupplierRepository(NimbDbContext context) : base(context)
        {
            _context = context;
        }

        public Supplier FindById(int id)
        {
            if (id > 0)
            {
                return _context.Suppliers.Find(id);
            }
            else
            {
                return _context.Suppliers.FirstOrDefault();
            }
        }

        public void Update(Supplier? obj)
        {
            var objFromDb = _context.Suppliers!.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {
                    objFromDb.Name = obj.Name;
                    objFromDb.Number = obj.Number;
                    objFromDb.Address = obj.Address;
                    objFromDb.EmailAddress = obj.EmailAddress;
                }
            }
        }

    }
}
