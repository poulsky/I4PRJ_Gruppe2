namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        StreetName = c.String(),
                        StreetNumber = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Coordinate_Latitude = c.Double(nullable: false),
                        Coordinate_Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.BarterAdds",
                c => new
                    {
                        BarterAddId = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Description = c.String(),
                        Picture = c.Binary(),
                        Thumbnail = c.Binary(),
                        Category = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BarterAddId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(maxLength: 500),
                        CreatedDateTime = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        BarterAdd_BarterAddId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.BarterAdds", t => t.BarterAdd_BarterAddId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.BarterAdd_BarterAddId);
            
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TradeHistories", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeHistory_TradeHistoryId", "dbo.TradeHistories");
            DropForeignKey("dbo.TradeRequestTradeHistories", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropForeignKey("dbo.TradeRequestBarterAdds", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.TradeRequestBarterAdds", "TradeRequest_TradeRequestId", "dbo.TradeRequests");
            DropForeignKey("dbo.TradeRequests", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TradeHistories", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BarterAdds", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeHistory_TradeHistoryId" });
            DropIndex("dbo.TradeRequestTradeHistories", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeRequestBarterAdds", new[] { "TradeRequest_TradeRequestId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.TradeRequests", new[] { "ApplicationUserId" });
            DropIndex("dbo.TradeHistories", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.TradeHistories", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.BarterAdds", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_AddressId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.TradeRequestTradeHistories");
            DropTable("dbo.TradeRequestBarterAdds");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.TradeRequests");
            DropTable("dbo.TradeHistories");
            DropTable("dbo.Comments");
            DropTable("dbo.BarterAdds");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
