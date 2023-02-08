﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetFirstOrDefault(Expression<Func<T?, bool>>? filter, string? includeProperties = null, bool tracked = true);
        Task<IEnumerable<T>> GetAll(Expression<Func<T?, bool>>? filter = null, string? includeProperties = null);
        void Add(T? entity);
        void Remove(T? entity);
        void RemoveRange(IEnumerable<T>? entity);
    }
}
