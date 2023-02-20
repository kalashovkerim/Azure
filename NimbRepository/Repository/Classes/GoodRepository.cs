using NimbRepository.DbContexts;
using NimbRepository.Model.Storekeeper;
using NimbRepository.Repository.Interfaces;

namespace NimbRepository.Repository.Classes
{
    public class GoodRepository : Repository<Good>, IGoodRepository
    {
        private NimbDataBaseContext _context;

        public GoodRepository(NimbDataBaseContext context) : base(context)
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

        public void Update(Good? obj)
        {
            var objFromDb = _context.Goods.FirstOrDefault(u => u.Id == obj.Id);
            if (obj != null)
            {
                if (objFromDb != null)
                {

                    objFromDb.SupplierId = obj.SupplierId;
                    objFromDb.Name = obj.Name;
                    objFromDb.PurchasePrice = obj.PurchasePrice;
                    objFromDb.Rate = obj.Rate;
                    objFromDb.Count = obj.Count;
                    objFromDb.Category = obj.Category;
                    objFromDb.Description = obj.Description;

                }
            }
        }

    }
}
