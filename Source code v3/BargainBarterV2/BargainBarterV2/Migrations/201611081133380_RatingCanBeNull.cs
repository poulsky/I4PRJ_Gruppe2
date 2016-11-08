namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingCanBeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Double(nullable: false));
        }
    }
}
