namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rating");
        }
    }
}
