using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Models
{
    public interface IBlogItemRepository
    {
        BlogItem FindById(int id);
        BlogItem Delete(int id);
        BlogItem Add(BlogItem blogItem);
        BlogItem Update(BlogItem blogItem);
        BlogItem Save(BlogItem item);
        IList<BlogItem> FindAll();
        IList<BlogItem> FindPage(int page, int size);
        // IQueryable<BlogItem> BlogItems { get; }

        void addTagToBlogItem(int blogItemId, int tagId);
    }

}
