using NimbRepository.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> FindById(int id);
        public void Update(User? obj); //?
    }
}
