namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    CommentId = c.Int(nullable: false, identity: true),
                    CommentText = c.String(),
                    CreatedDateTime = c.DateTime(nullable: false),
                    ApplicationUser_Id = c.String(maxLength: 128),
                    BarterAdd_BarterAddId = c.Int(),
                })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.BarterAdds", t => t.BarterAdd_BarterAddId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.BarterAdd_BarterAddId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "BarterAdd_BarterAddId", "dbo.BarterAdds");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "BarterAdd_BarterAddId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Comments");
        }
    }
}
