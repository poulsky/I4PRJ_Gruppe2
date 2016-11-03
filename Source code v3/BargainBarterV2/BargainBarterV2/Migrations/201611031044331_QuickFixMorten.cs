namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuickFixMorten : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BarterAdds", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropIndex("dbo.BarterAdds", new[] { "TradeRequest_TradeRequestId" });
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
            
            AddColumn("dbo.TradeHistories", "BarterAdd_BarterAddId", c => c.Int());
            CreateIndex("dbo.TradeHistories", "BarterAdd_BarterAddId");
            AddForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds", "BarterAddId");
            DropColumn("dbo.BarterAdds", "TradeRequest_TradeRequestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BarterAdds", "TradeRequest_TradeRequestId", c => c.Int());
            DropForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestBarterAdds", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestBarterAdds", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.TradeHistories", new[] { "BarterAdd_BarterAddId" });
            DropColumn("dbo.TradeHistories", "BarterAdd_BarterAddId");
            DropTable("dbo.TradeRequestBarterAdds");
            CreateIndex("dbo.BarterAdds", "TradeRequest_TradeRequestId");
            AddForeignKey("dbo.BarterAdds", "TradeRequest_TradeRequestId", "dbo.TradeRequests", "TradeRequestId");
        }
    }
}
