﻿using Microsoft.EntityFrameworkCore;
using NimbRepository.DbContexts;
using NimbRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NimbDbContext? _context;
        internal DbSet<T>? contextSet;

        public Repository(NimbDbContext? context)
        {
            _context = context;
            this.contextSet = _context!.Set<T>();
        }
        public void Add(T? entity)
        {
            contextSet!.Add(entity!);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T>? query = contextSet!;
            if (filter != null)
            {
                query = query!.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query!.Include(includeProp);
                }
            }
            return query.ToList()!;
        }

        public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T>? query = contextSet!;

                query = query!.Where(filter!);

                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query!.Include(includeProp);
                    }
                }
                return await query!.FirstOrDefaultAsync();
            }
            else
            {
                IQueryable<T>? query = contextSet!.AsNoTracking();

                query = query!.Where(filter!);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query!.Include(includeProp);
                    }
                }
                return await query!.FirstOrDefaultAsync();
            }

        }

        public void Remove(T? entity)
        {
            contextSet!.Remove(entity!);
        }

        public void RemoveRange(IEnumerable<T>? entity)
        {
            contextSet!.RemoveRange(entity!);
        }

    }
}
