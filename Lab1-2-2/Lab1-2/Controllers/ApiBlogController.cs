using Lab1_2.Exception;
using Lab1_2.Filter;
using Lab1_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ApiBlogController : Controller
    {  
            private IBlogItemRepository items;

            public ApiBlogController(IBlogItemRepository items)
            {
                this.items = items;
            }

            [HttpGet]
            public IList<BlogItem> GetAll()
            {
                return items.FindAll();
            }


            [HttpGet()]
            [Route("{id}")]
            [DisableBasicAuthentication]
            public ActionResult GetOne(int id)
            {
                BlogItem blogItem = items.FindById(id);
                if (blogItem != null)
                {
                    return new OkObjectResult(blogItem);
                }
                else
                {
                    throw new MyException("Brak identyfikatora zasobu!");
                }
            }


            [HttpPost]
            public ActionResult Add([FromBody] BlogItem item)
            {
                if (ModelState.IsValid)
                {
                    BlogItem blogItem = items.Save(item);
                    return new CreatedResult($"/api/items/{blogItem.Id}", blogItem);
                }
                else
                {
                    return BadRequest();
                }
            }

            [HttpDelete]
            [Route("{id}")]
            public ActionResult Delete(int id)
            {
                if (items.FindById(id) != null)
                {
                    items.Delete(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }

            [HttpPut]
            [Route("{id}")]
            public ActionResult Update(int id, [FromBody] BlogItem item)
            {
                item.Id = id;
                BlogItem blogItem = items.Update(item);
                if (blogItem == null)
                {
                    return NotFound();
                }
                else
                {
                    return NoContent();
                }
            }

        }

    
}
