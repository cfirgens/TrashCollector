namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchedFKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "AddressId" });
            AddColumn("dbo.Addresses", "CustomerId", c => c.Int());
            CreateIndex("dbo.Addresses", "CustomerId");
            AddForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers", "CustomerId");
            DropColumn("dbo.Customers", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "AddressId", c => c.Int());
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropColumn("dbo.Addresses", "CustomerId");
            CreateIndex("dbo.Customers", "AddressId");
            AddForeignKey("dbo.Customers", "AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
