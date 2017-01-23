namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableAssignment1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Attachment", c => c.String());
            DropColumn("dbo.Assignments", "Upload");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Upload", c => c.String());
            DropColumn("dbo.Assignments", "Attachment");
        }
    }
}
