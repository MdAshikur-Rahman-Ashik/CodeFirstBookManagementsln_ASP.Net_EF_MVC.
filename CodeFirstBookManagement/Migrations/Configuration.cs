namespace CodeFirstBookManagement.Migrations
{
    using CodeFirstBookManagement.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstBookManagement.DAL.BookDBEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirstBookManagement.DAL.BookDBEntities context)
        {
            var books = new List<Book>
            {
                new Book{BookName="HTML"},
                 new Book{BookName="CSharp"},
                  new Book{BookName="JavaCookBook"},
                   new Book{BookName="XML"},
                    new Book{BookName="CF"},

            };
            books.ForEach(c => context.Books.AddOrUpdate(c));
            context.SaveChanges();
        }
    }
}
