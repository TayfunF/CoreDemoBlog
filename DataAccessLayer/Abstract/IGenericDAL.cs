using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDAL<T> where T: class
    {
        //Ekleme
        void Add(T t);
        //Silme
        void Delete(T t);
        //Güncelleme
        void Update(T t);
        //Listeleme
        //Direkt listeleme için kullanılan
        List<T> GetAllList();
        //Bu ise istenilen herhangi bir niteliğe göre arama yapıyor.
        //_context.table.where(m=> m.id == id) ile aynı şey bu
        public List<T> Where(Expression<Func<T, bool>> predicate);
        //ID ye göre işlemler yapabilmek için
        T GetByID(int id);
    }
}