using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public ISupplierRepository Supplier { get; }
        public IGoodRepository Good { get;}
        public IClientRepository Client { get; }
        public ICompanyRepository Company { get; }
        void Save();
    }
}
