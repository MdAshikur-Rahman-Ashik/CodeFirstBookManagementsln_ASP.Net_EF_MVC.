using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.Models
{
    public class Customer
    {
        public Customer()
        {

            this.BookEntries = new HashSet<BookEntry>();
        }
        public int CustomerId { get; set; }
        [Required, StringLength(40, ErrorMessage = "*Name must be between 40 letter"), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }
        public bool IsRegular { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public int Diposite { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }
    }
}