namespace DoctorImages.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "Data", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageModels", "Data");
        }
    }
}
