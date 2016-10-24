namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thumbnails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BarterAdds", "Thumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BarterAdds", "Thumbnail");
        }
    }
}
