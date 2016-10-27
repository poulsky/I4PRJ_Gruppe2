namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoordinatesAddedToAdress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Coordinate_Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Addresses", "Coordinate_Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Coordinate_Longitude");
            DropColumn("dbo.Addresses", "Coordinate_Latitude");
        }
    }
}
