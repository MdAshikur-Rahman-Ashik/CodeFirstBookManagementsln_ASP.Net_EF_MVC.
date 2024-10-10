using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.Models.ViewModel
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            this.BookList = new List<int>();
        }
        public int CustomerId { get; set; }
        [Required, StringLength(40, ErrorMessage = "*Name must be between 40 letter"), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, Display(Name = "Date of birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidDate]
        public System.DateTime BirthDate { get; set; }
        
        [RegularExpression("^[a-zA-Z0-9][-a-zA-Z0-9._]+@([-a-zA-Z0-9]+\\.)+[a-zA-Z]{2,10}$", ErrorMessage = "Wrong format")]
        public string Email { get; set; }
        [Range(1, 20000, ErrorMessage = "*Must be between 1 to 20000"),]
        public int Diposite { get; set; }
        public bool IsRegular { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase PicturePath { get; set; }
        public List<int> BookList { get; set; }
    }
}