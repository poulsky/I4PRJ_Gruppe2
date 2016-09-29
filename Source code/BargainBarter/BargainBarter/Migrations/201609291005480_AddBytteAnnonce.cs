namespace BargainBarter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBytteAnnonce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BytteAnnonces",
                c => new
                    {
                        BytteAnnonceId = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Beskrivelse = c.String(),
                        Bruger_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BytteAnnonceId)
                .ForeignKey("dbo.AspNetUsers", t => t.Bruger_Id)
                .Index(t => t.Bruger_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BytteAnnonces", "Bruger_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BytteAnnonces", new[] { "Bruger_Id" });
            DropTable("dbo.BytteAnnonces");
        }
    }
}
