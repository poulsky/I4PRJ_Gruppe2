namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Int(nullable: false));
        }
    }
}
