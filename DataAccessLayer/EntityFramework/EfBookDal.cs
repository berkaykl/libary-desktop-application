using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBookDal : GenericRepository<Book>, IBookDal
    {
        public List<object> GetBooksWithCategory()
        {
            var context = new TestContext();
            var values = context.Books
                .Select(x => new
                {
                    BookId = x.BookId,
                    BookName = x.BookName,
                    BookDescription = x.BookDescription,
                    CategoryName = x.Category.CategoryName,
                    AuthorName = x.Author.AuthorName + " " + x.Author.AuthorLastname
                }).ToList();
            return values.Cast<object>().ToList();
        }
    }
}
