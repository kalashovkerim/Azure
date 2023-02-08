using NimbRepository.Model.Admin;
using NimbRepository.Model.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public Task<Company?> FindById(int id);
        public void Update(Company? obj); //?
    }
}
