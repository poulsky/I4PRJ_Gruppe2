namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishedTradeAndRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropIndex("dbo.TradeHistories", new[] { "ApplicationUserId" });
            DropIndex("dbo.TradeHistories", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeHistory_TradeHistoryId" });
            RenameColumn(table: "dbo.TradeHistories", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            CreateTable(
                "dbo.FinishedTrades",
                c => new
                    {
                        FinishedTradeId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.FinishedTradeId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingValue = c.Int(nullable: false),
                        RatingComment = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        FinishedTrade_FinishedTradeId = c.Int(),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.FinishedTrades", t => t.FinishedTrade_FinishedTradeId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.FinishedTrade_FinishedTradeId);
            
            CreateTable(
                "dbo.FinishedTradeTradeHistories",
                c => new
                    {
                        FinishedTrade_FinishedTradeId = c.Int(nullable: false),
                        TradeHistory_TradeHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FinishedTrade_FinishedTradeId, t.TradeHistory_TradeHistoryId })
                .ForeignKey("dbo.FinishedTrades", t => t.FinishedTrade_FinishedTradeId, cascadeDelete: true)
                .ForeignKey("dbo.TradeHistories", t => t.TradeHistory_TradeHistoryId, cascadeDelete: true)
                .Index(t => t.FinishedTrade_FinishedTradeId)
                .Index(t => t.TradeHistory_TradeHistoryId);
            
            AddColumn("dbo.BarterAdds", "Traded", c => c.Boolean(nullable: false));
            AddColumn("dbo.BarterAdds", "FinishedTrade_FinishedTradeId", c => c.Int());
            AddColumn("dbo.TradeRequests", "TradeHistory_TradeHistoryId", c => c.Int());
            AlterColumn("dbo.TradeHistories", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BarterAdds", "FinishedTrade_FinishedTradeId");
            CreateIndex("dbo.TradeRequests", "TradeHistory_TradeHistoryId");
            CreateIndex("dbo.TradeHistories", "ApplicationUser_Id");
            AddForeignKey("dbo.BarterAdds", "FinishedTrade_FinishedTradeId", "dbo.FinishedTrades", "FinishedTradeId");
            AddForeignKey("dbo.TradeRequests", "TradeHistory_TradeHistoryId", "dbo.TradeHistories", "TradeHistoryId");
            DropColumn("dbo.TradeHistories", "BarterAdd_BarterAddId");
            DropTable("dbo.TradeRequestTradeHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TradeRequestTradeHistories",
                c => new
                    {
                        TradeRequest_TradeRequestId = c.Int(nullable: false),
                        TradeHistory_TradeHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TradeRequest_TradeRequestId, t.TradeHistory_TradeHistoryId });
            
            AddColumn("dbo.TradeHistories", "BarterAdd_BarterAddId", c => c.Int());
            DropForeignKey("dbo.TradeRequests", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.FinishedTradeTradeHistories", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.FinishedTradeTradeHistories", "FinishedTrade_FinishedTradeId", "dbo.FinishedTrades");
            DropForeignKey("dbo.Ratings", "FinishedTrade_FinishedTradeId", "dbo.FinishedTrades");
            DropForeignKey("dbo.Ratings", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BarterAdds", "FinishedTrade_FinishedTradeId", "dbo.FinishedTrades");
            DropIndex("dbo.FinishedTradeTradeHistories", new[] { "TradeHistory_TradeHistoryId" });
            DropIndex("dbo.FinishedTradeTradeHistories", new[] { "FinishedTrade_FinishedTradeId" });
            DropIndex("dbo.Ratings", new[] { "FinishedTrade_FinishedTradeId" });
            DropIndex("dbo.Ratings", new[] { "ApplicationUserId" });
            DropIndex("dbo.TradeHistories", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TradeRequests", new[] { "TradeHistory_TradeHistoryId" });
            DropIndex("dbo.BarterAdds", new[] { "FinishedTrade_FinishedTradeId" });
            AlterColumn("dbo.TradeHistories", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.TradeRequests", "TradeHistory_TradeHistoryId");
            DropColumn("dbo.BarterAdds", "FinishedTrade_FinishedTradeId");
            DropColumn("dbo.BarterAdds", "Traded");
            DropTable("dbo.FinishedTradeTradeHistories");
            DropTable("dbo.Ratings");
            DropTable("dbo.FinishedTrades");
            RenameColumn(table: "dbo.TradeHistories", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            CreateIndex("dbo.TradeRequestTradeHistories", "TradeHistory_TradeHistoryId");
            CreateIndex("dbo.TradeRequestTradeHistories", "TradeRequest_TradeRequestId");
            CreateIndex("dbo.TradeHistories", "BarterAdd_BarterAddId");
            CreateIndex("dbo.TradeHistories", "ApplicationUserId");
            AddForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds", "BarterAddId");
            AddForeignKey("dbo.TradeRequestTradeHistories", "TradeHistory_TradeHistoryId", "dbo.TradeHistories", "TradeHistoryId", cascadeDelete: true);
            AddForeignKey("dbo.TradeRequestTradeHistories", "TradeRequest_TradeRequestId", "dbo.TradeRequests", "TradeRequestId", cascadeDelete: true);
        }
    }
}
