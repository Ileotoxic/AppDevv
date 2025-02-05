﻿using AppDev2.Data;
using AppDev2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppDev2.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate, string? includeProperty = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);
            if (!String.IsNullOrEmpty(includeProperty))
            {
                query.Include(includeProperty).ToList();
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includedProperty = null)
        {
            IQueryable<T> query = _dbSet;
            if (!String.IsNullOrEmpty(includedProperty))
            {
                query.Include(includedProperty).ToList();
            }
            return query.ToList();
        }

    }
}