using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.Models
{
    public class Book
    {
        public Book()
        {
            this.BookEntries = new HashSet<BookEntry>();
        }
        public int BookId { get; set; }
        [Required, Display(Name = "Book Name")]
        [StringLength(50)]
        public string BookName { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }


    }
}