namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LazyLoadingAdd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BarterAdds", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.BarterAdds", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BarterAdds", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.BarterAdds", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
