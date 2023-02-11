using NimbRepository.DbContexts;
using NimbRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IGoodRepository Good { get; private set; }
        public IClientRepository Client { get; private set; }

        public ICompanyRepository Company { get; private set; }

        private NimbDbContext _context;
        public UnitOfWork(NimbDbContext context)
        {
            _context = context;

            User = new UserRepository(_context);
            Supplier = new SupplierRepository(_context);
            Good = new GoodRepository(_context);
            Client = new ClientRepository(_context);
            Company = new CompanyRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
