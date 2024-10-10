namespace CodeFirstBookManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEntries",
                c => new
                    {
                        BookEntryId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookEntryId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 40),
                        BirthDate = c.DateTime(nullable: false),
                        IsRegular = c.Boolean(nullable: false),
                        Picture = c.String(),
                        Email = c.String(),
                        Diposite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookEntries", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BookEntries", "BookId", "dbo.Books");
            DropIndex("dbo.BookEntries", new[] { "CustomerId" });
            DropIndex("dbo.BookEntries", new[] { "BookId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Books");
            DropTable("dbo.BookEntries");
        }
    }
}
