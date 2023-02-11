using NimbRepository.Model.Storekeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        public Task<Good> FindById(int id);
        public void Update(Good? obj); 
    }
}
