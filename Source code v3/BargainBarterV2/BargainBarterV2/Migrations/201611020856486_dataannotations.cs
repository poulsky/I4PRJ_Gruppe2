namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataannotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BarterAdds", "Titel", c => c.String(nullable: false));
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BarterAdds", "Titel", c => c.String());
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
