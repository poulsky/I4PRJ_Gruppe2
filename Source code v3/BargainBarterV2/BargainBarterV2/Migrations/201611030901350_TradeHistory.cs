namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TradeHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradeHistories",
                c => new
                    {
                        TradeHistoryId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        BarterAdd_BarterAddId = c.Int(),
                    })
                .PrimaryKey(t => t.TradeHistoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BarterAdds", t => t.BarterAdd_BarterAddId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BarterAdd_BarterAddId);
            
            CreateTable(
                "dbo.TradeRequests",
                c => new
                    {
                        TradeRequestId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        RequestStates = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TradeRequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.TradeRequestBarterAdds",
                c => new
                    {
                        TradeRequest_TradeRequestId = c.Int(nullable: false),
                        BarterAdd_BarterAddId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TradeRequest_TradeRequestId, t.BarterAdd_BarterAddId })
                .ForeignKey("dbo.TradeRequests", t => t.TradeRequest_TradeRequestId, cascadeDelete: true)
                .ForeignKey("dbo.BarterAdds", t => t.BarterAdd_BarterAddId, cascadeDelete: true)
                .Index(t => t.TradeRequest_TradeRequestId)
                .Index(t => t.BarterAdd_BarterAddId);
            
            CreateTable(
                "dbo.TradeRequestTradeHistories",
                c => new
                    {
                        TradeRequest_TradeRequestId = c.Int(nullable: false),
                        TradeHistory_TradeHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TradeRequest_TradeRequestId, t.TradeHistory_TradeHistoryId })
                .ForeignKey("dbo.TradeRequests", t => t.TradeRequest_TradeRequestId, cascadeDelete: true)
                .ForeignKey("dbo.TradeHistories", t => t.TradeHistory_TradeHistoryId, cascadeDelete: true)
                .Index(t => t.TradeRequest_TradeRequestId)
                .Index(t => t.TradeHistory_TradeHistoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropForeignKey("dbo.TradeRequestBarterAdds", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestBarterAdds", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropForeignKey("dbo.TradeRequests", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TradeHistories", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeHistory_TradeHistoryId" });
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.TradeRequests", new[] { "ApplicationUserId" });
            DropIndex("dbo.TradeHistories", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeHistories", new[] { "ApplicationUserId" });
            DropTable("dbo.TradeRequestTradeHistories");
            DropTable("dbo.TradeRequestBarterAdds");
            DropTable("dbo.TradeRequests");
            DropTable("dbo.TradeHistories");
        }
    }
}
