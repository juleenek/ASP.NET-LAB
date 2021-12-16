using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Models
{
    public class ItemBlogRepository : IBlogItemRepository
    {
        private ApplicationDbContext _context;
        public ItemBlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public BlogItem FindById(int id)
        {
            _context.Find(typeof(BlogItem), id);
            return _context.BlogItems.Find(id);
        }
        public BlogItem Delete(int id)
        {
            var item = _context.BlogItems.Remove(FindById(id)).Entity;
            _context.SaveChanges();

            return item;
        }
        public BlogItem Add(BlogItem item)
        {
            var entity = _context.BlogItems.Add(item).Entity;
            _context.SaveChanges();

            return entity;
        }
        public BlogItem Update(BlogItem product)
        {
            var entity = _context.BlogItems.Update(product).Entity;
            _context.SaveChanges();

            return entity;
        }
        public BlogItem Save(BlogItem item)
        {
            var entryEntity = _context.BlogItems.Add(item);
            _context.SaveChanges();
            return entryEntity.Entity;
        }
        public IList<BlogItem> FindAll()
        {
            return _context.BlogItems.ToList();
        }
        public IList<BlogItem> FindPage(int page, int size)
        {
            return (from item in _context.BlogItems orderby item.CreationTimstamp select item)
                 .Skip(page * size)
                 .Take(size)
                 .ToList();
        }
        //public IQueryable<BlogItem> BlogItems => _context.BlogItems;

        public void addTagToBlogItem(int blogItemId, int tagId)
        {
            var item = _context.BlogItems.Find(blogItemId);
            var tag = _context.Tags.Find(tagId);
            item.Tags.Add(tag);

            Update(item);
        }
    }
}
