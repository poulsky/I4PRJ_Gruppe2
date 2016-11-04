namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requiredtagfjernet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
