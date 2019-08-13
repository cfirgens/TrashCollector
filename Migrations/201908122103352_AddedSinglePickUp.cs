namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSinglePickUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "SingleDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "SingleDate");
        }
    }
}
