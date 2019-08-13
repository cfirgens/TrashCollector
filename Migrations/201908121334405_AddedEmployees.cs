namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        employeeNumber = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        zipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.employeeNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
