namespace BargainBarter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserExtras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Vejnavn", c => c.String());
            AddColumn("dbo.AspNetUsers", "Husnummer", c => c.String());
            AddColumn("dbo.AspNetUsers", "Postnummer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Postnummer");
            DropColumn("dbo.AspNetUsers", "Husnummer");
            DropColumn("dbo.AspNetUsers", "Vejnavn");
        }
    }
}
