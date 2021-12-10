using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Models { 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<BlogItem> BlogItems { get; set; }
        public DbSet<Tag> Tags { get; set; } // !!!
    }
    public interface IBlogItemRepository
    {
        BlogItem FindById(int id);
        BlogItem Delete(int id);
        BlogItem Add(BlogItem blogItem);
        BlogItem Update(BlogItem blogItem);
        IList<BlogItem> FindAll();
        IList<BlogItem> FindPage(int page, int size);
        // IQueryable<BlogItem> BlogItems { get; }

        void addTagToBlogItem(int blogItemId, int tagId);
    }
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
    public class BlogItem
    {
        public BlogItem()
        {
            Tags = new HashSet<Tag>(); // HashSet żeby się nie powtarzało
        }
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł")]
        [MinLength(length:5, ErrorMessage = "Treść musi mieć przynajmniej 5 znaków")]
        public string Content { get; set; }
        public DateTime CreationTimstamp { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
