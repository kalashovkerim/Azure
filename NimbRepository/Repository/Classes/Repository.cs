using Microsoft.EntityFrameworkCore;
using NimbRepository.DbContexts;
using NimbRepository.Repository.Interfaces;
using System.Linq.Expressions;


namespace NimbRepository.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NimbDataBaseContext _context;
        internal DbSet<T> contextSet;

        public Repository(NimbDataBaseContext? context)
        {
            _context = context;
            this.contextSet = _context.Set<T>();
        }
        public void Add(T? entity)
        {
            contextSet.Add(entity!);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = contextSet!;
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

        public T? GetFirstOrDefault(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T> query = contextSet!;

                query = query!.Where(filter!);

                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query!.Include(includeProp);
                    }
                }
                return query!.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = contextSet.AsNoTracking();

                query = query!.Where(filter!);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query!.Include(includeProp);
                    }
                }
                return query!.FirstOrDefault();
            }

        }

        public void Remove(T? entity)
        {
            if(entity != null)
            {
                contextSet.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<T>? entity)
        {
            if (entity != null)
            {
                contextSet.RemoveRange(entity);
            }
           
        }

    }
}
