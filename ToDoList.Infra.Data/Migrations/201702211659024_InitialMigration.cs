namespace ToDoList.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Attachment = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);                        
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "UserId" });            
            DropTable("dbo.Assignments");
        }
    }
}
