namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChangesinstructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "IsPayed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tickets", "ConcessionId_ConcessionId", c => c.Int());
            CreateIndex("dbo.Tickets", "ConcessionId_ConcessionId");
            AddForeignKey("dbo.Tickets", "ConcessionId_ConcessionId", "dbo.Concessions", "ConcessionId");
            DropColumn("dbo.Reservations", "IsPay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "IsPay", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Tickets", "ConcessionId_ConcessionId", "dbo.Concessions");
            DropIndex("dbo.Tickets", new[] { "ConcessionId_ConcessionId" });
            DropColumn("dbo.Tickets", "ConcessionId_ConcessionId");
            DropColumn("dbo.Reservations", "IsPayed");
        }
    }
}
