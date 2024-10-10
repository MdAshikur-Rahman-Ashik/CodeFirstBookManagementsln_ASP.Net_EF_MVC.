using CodeFirstBookManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirstBookManagement.DAL
{
    public partial class BookDBEntities : DbContext
    {
        public BookDBEntities(): base("BookDBEntities")
        {


        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookEntry> BookEntries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}