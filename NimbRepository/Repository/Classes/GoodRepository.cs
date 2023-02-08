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
    public class GoodRepository : Repository<Good>, IGoodRepository
    {
        private NimbDbContext? _context;

        public GoodRepository(NimbDbContext? context) : base(context)
        {
            _context = context;
        }

        public async Task<Good> FindById(int id)
        {
            if (id > 0)
            {
                return _context.Goods.Find(id)!;
            }
            else
            {
                return _context.Goods.FirstOrDefault();
            }
        }

        public void Update(Good obj)
        {
            var objFromDb = _context.Goods!.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {

                    objFromDb.SupplierId = obj.SupplierId;
                    objFromDb.Name = obj.Name;
                    objFromDb.Price = obj.Price;
                    objFromDb.Count = obj.Count;
                    objFromDb.Category = obj.Category;
                    objFromDb.Description = obj.Description;

                }
            }
        }

    }
}
