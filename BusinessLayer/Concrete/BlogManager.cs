using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDAL _blogDAL;

        public BlogManager(IBlogDAL blogDAL)
        {
            _blogDAL = blogDAL;
        } 

        public void BlogAdd(Blog blog)
        {
            _blogDAL.Add(blog);
        }

        public void BlogDelete(Blog blog)
        {
            _blogDAL.Delete(blog);
        }

        public void BlogUpdate(Blog blog)
        {
            _blogDAL.Update(blog);
        }

        public Blog GetByID(int id)
        {
            return _blogDAL.GetByID(id);
        }

        public List<Blog> GetList()
        {
            return _blogDAL.GetAllList();
        }
    }
}
