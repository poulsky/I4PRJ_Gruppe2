namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manglendemigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BarterAdds", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BarterAdds", "CreatedDateTime");
        }
    }
}
