namespace MVCDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Maintenance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maintenance",
                c => new
                    {
                        MaintenanceId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.MaintenanceId);
            
            AddColumn("dbo.Car", "ImageContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Car", "ImageFileName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Car", "ImageFileName");
            DropColumn("dbo.Car", "ImageContentType");
            DropTable("dbo.Maintenance");
        }
    }
}
