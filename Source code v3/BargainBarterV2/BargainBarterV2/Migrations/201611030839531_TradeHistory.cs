namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TradeHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TradeHistoryBarterAdds", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.TradeHistoryBarterAdds", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            DropIndex("dbo.TradeHistoryBarterAdds", new[] { "TradeHistory_TradeHistoryId" });
            DropIndex("dbo.TradeHistoryBarterAdds", new[] { "BarterAdd_BarterAddId" });
            AddColumn("dbo.TradeHistories", "BarterAdd_BarterAddId", c => c.Int());
            AlterColumn("dbo.BarterAdds", "Titel", c => c.String(nullable: false));
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            CreateIndex("dbo.TradeHistories", "BarterAdd_BarterAddId");
            AddForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds", "BarterAddId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropTable("dbo.TradeHistoryBarterAdds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TradeHistoryBarterAdds",
                c => new
                    {
                        TradeHistory_TradeHistoryId = c.Int(nullable: false),
                        BarterAdd_BarterAddId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TradeHistory_TradeHistoryId, t.BarterAdd_BarterAddId });
            
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropIndex("dbo.TradeHistories", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BarterAdds", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BarterAdds", "Titel", c => c.String());
            DropColumn("dbo.TradeHistories", "BarterAdd_BarterAddId");
            CreateIndex("dbo.TradeHistoryBarterAdds", "BarterAdd_BarterAddId");
            CreateIndex("dbo.TradeHistoryBarterAdds", "TradeHistory_TradeHistoryId");
            CreateIndex("dbo.BarterAdds", "ApplicationUserId");
            AddForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TradeHistoryBarterAdds", "BarterAdd_BarterAddId", "dbo.BarterAdds", "BarterAddId", cascadeDelete: true);
            AddForeignKey("dbo.TradeHistoryBarterAdds", "TradeHistory_TradeHistoryId", "dbo.TradeHistories", "TradeHistoryId", cascadeDelete: true);
        }
    }
}
