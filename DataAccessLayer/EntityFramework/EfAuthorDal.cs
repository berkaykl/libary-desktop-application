using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAuthorDal : GenericRepository<Author>, IAuthorDal
    {
        public List<Object> GetFullName()
        {
            var context = new TestContext();

            var values = context.Authors.Select(x => new
            {
                AuthorId = x.AuthorId,
                FullName = x.AuthorName + " " + x.AuthorLastname
            }).ToList();

            return values.Cast<object>().ToList();
        }
    }
}
