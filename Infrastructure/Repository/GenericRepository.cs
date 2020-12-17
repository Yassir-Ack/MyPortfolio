using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;

        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T Entity)
        {
            table.Add(Entity);
        }

        public void Update(T Entity)
        {
            table.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }
    }
}
