namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Upload", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "Upload");
        }
    }
}
