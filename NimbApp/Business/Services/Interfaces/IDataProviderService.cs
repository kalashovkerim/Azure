using NimbRepository.Model;
using NimbRepository.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IDataProviderService
    {
        public IEnumerable<IEntity> GetAllData();
        public IEntity GetData(int id);
        public void SetData(IEntity entity);
        public void Save();
        public void Delete(int id);
        public IEntity FindById(int id);
        public void Update(User? obj); // only for user
    }
}
