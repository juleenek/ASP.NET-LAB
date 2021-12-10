using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Models
{
    public class Tag
    {
        public Tag()
        {
            BlogItems = new HashSet<BlogItem>(); // zainicjowanie kolekcji
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogItem> BlogItems { get; set; }
    }
}
