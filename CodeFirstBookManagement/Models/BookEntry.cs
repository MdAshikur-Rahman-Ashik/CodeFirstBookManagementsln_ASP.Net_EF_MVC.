using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.Models
{
    public class BookEntry
    {
        public int BookEntryId { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }

    }
}