namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoolPickup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Addresses", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Addresses", "PickedUp", c => c.Boolean());
            DropColumn("dbo.Addresses", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Addresses", "PickedUp");
            CreateIndex("dbo.Addresses", "ApplicationUser_Id");
            AddForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
