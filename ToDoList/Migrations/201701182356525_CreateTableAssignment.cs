namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableAssignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UsuarioId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "UsuarioId" });
            DropTable("dbo.Assignments");
        }
    }
}
