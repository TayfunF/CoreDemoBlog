using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
  public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Add(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Update(t);
            _context.SaveChanges();
        }

        public List<T> GetAllList()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Where(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
