using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Models { 
    
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
