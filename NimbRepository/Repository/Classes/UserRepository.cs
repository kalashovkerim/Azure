﻿using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Classes
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private NimbDbContext? _context;

        public UserRepository(NimbDbContext? context) : base(context)
        {
            _context = context;
        }

        public User? FindById(int id)
        {
            return _context!.Users!.Find(id)!;
        }

        public void Update(User? obj)
        {
            var objFromDb = _context!.Users!.FirstOrDefault(u => u.Id == obj.Id);

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
