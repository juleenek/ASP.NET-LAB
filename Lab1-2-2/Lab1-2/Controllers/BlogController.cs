using Lab1_2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Controllers
{
    public class BlogController : Controller
    {
        private IBlogItemRepository repository;
        public BlogController(IBlogItemRepository repository)
        {
            this.repository = repository;
        }
        //static List<BlogItem> items = new List<BlogItem>()
        //{
        //    new BlogItem(){Title="Programowanie", Id = 1, Content="Przykładowa treść", CreationTimstamp = DateTime.Now},
        //    new BlogItem(){Title="ASP", Id = 2, Content="Przykładowa treść", CreationTimstamp = DateTime.Now},
        //};
        //static int index = 2;
        public IActionResult Edit(int id)
        {
            BlogItem editedItem = null;
            foreach (var item in repository.FindAll())
            {
                if (item.Id == id)
                {
                    editedItem = item;
                    break;
                }
            }
            return View("EditForm", editedItem);
        }
        [HttpPost]
        public IActionResult Edit(BlogItem itemFromForm)
        {
            int id = itemFromForm.Id;
            BlogItem originItem = findById(id);
            originItem.Content = itemFromForm.Content;
            originItem.Title = itemFromForm.Title;
            repository.Update(itemFromForm);

            return View("BlogList", repository.FindAll());
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddForm()
        {
            return View();
        }
        public IActionResult BlogList()
        {

            return View(repository.FindAll());
        }
        public IActionResult Add(BlogItem item)
        {
            if (ModelState.IsValid)
            {
                item.CreationTimstamp = DateTime.Now;
                repository.Add(item);
                //items.Add(item);
                return View("ConfirmBlogItem", item);
            }
            else
            {
                return View("AddForm");
            }
        
        }
        public IActionResult Delete(int id)
        {
            repository.Delete(id);

            return View("BlogList", repository.FindAll());
        }

        public BlogItem findById(int id)
        {
            foreach (var item in repository.FindAll())
            {
                if (item.Id == id)
                {
                    return item;
                }

            }
            return null;
        }
    }
}
