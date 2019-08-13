namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStuffTOAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Addresses", "ApplicationUser_Id");
            AddForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Addresses", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Addresses", "ApplicationUser_Id");
        }
    }
}
