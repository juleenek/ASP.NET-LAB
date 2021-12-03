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
    }
    public interface IBlogItemRepository
    {
        BlogItem Find(int id);
        BlogItem Delete(int id);
        BlogItem Add(BlogItem blogItem);
        BlogItem Update(BlogItem blogItem);
        IList<BlogItem> FindAll();
        // IQueryable<BlogItem> BlogItems { get; }
    }
    public class ItemBlogRepository : IBlogItemRepository
    {
        private ApplicationDbContext _context;
        public ItemBlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public BlogItem Find(int id)
        {
            return _context.BlogItems.Find(id);
        }
        public BlogItem Delete(int id)
        {
            var item = _context.BlogItems.Remove(Find(id)).Entity; 
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
        //public IQueryable<BlogItem> BlogItems => _context.BlogItems;
    }
    public class BlogItem
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł")]
        [MinLength(length:5, ErrorMessage = "Treść musi mieć przynajmniej 5 znaków")]
        public string Content { get; set; }
        public DateTime CreationTimstamp { get; set; }


    }
}
