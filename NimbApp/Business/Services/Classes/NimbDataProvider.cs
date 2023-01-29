using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NimbRepository.DbContexts;
using NimbRepository.Model;
using NimbRepository.Model.Admin;

namespace Business.Services.Classes
{
    public class NimbDataProvider<T> : IDataProviderService where T : class, IEntity
    {
        private readonly NimbDbContext _context = new();

        private DbSet<T> _dbSet;

        public NimbDataProvider()
        {
            _dbSet = _context.Set<T>();
        }

        public void Delete(int id)
        {
            _context.Remove(_dbSet.Find(id));
        }

        public IEntity FindById(int id)
        {
            return _dbSet.Find(id)!;
        }

        public IEnumerable<IEntity> GetAllData()
        {
            return _dbSet.ToArray();
        }

        public IEntity GetData(int id)
        {
            return _dbSet.First(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SetData(IEntity entity)
        {
            _context.Add(entity);
        }
        public void Update(User? obj) // only for user
        {
            var objFromDb = _dbSet.FirstOrDefault(u => u.Id == obj.Id) as User;

            if (objFromDb != null)
            {

                objFromDb.FirstName = obj.FirstName;
                objFromDb.LastName = obj.LastName;
                objFromDb.Number = obj.Number;
                objFromDb.Address = obj.Address;
                objFromDb.EmailAddress = obj.EmailAddress;
                objFromDb.Password = obj.Password;
                objFromDb.PatronymicName = obj.PatronymicName;
                objFromDb.Position = obj.Position;
            }

        }

    }
}
