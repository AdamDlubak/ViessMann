namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesinTicket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "ConcessionId_ConcessionId", "dbo.Concessions");
            DropForeignKey("dbo.Tickets", "Concession_ConcessionId", "dbo.Concessions");
            DropForeignKey("dbo.Tickets", "Line_LineId", "dbo.Lines");
            DropIndex("dbo.Tickets", new[] { "Concession_ConcessionId" });
            DropIndex("dbo.Tickets", new[] { "ConcessionId_ConcessionId" });
            DropIndex("dbo.Tickets", new[] { "Line_LineId" });
            RenameColumn(table: "dbo.Tickets", name: "Concession_ConcessionId", newName: "ConcessionId");
            RenameColumn(table: "dbo.Tickets", name: "Line_LineId", newName: "LineId");
            AlterColumn("dbo.Tickets", "ConcessionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "LineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "ConcessionId");
            CreateIndex("dbo.Tickets", "LineId");
            AddForeignKey("dbo.Tickets", "ConcessionId", "dbo.Concessions", "ConcessionId", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "LineId", "dbo.Lines", "LineId", cascadeDelete: true);
            DropColumn("dbo.Tickets", "ConcessionId_ConcessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "ConcessionId_ConcessionId", c => c.Int());
            DropForeignKey("dbo.Tickets", "LineId", "dbo.Lines");
            DropForeignKey("dbo.Tickets", "ConcessionId", "dbo.Concessions");
            DropIndex("dbo.Tickets", new[] { "LineId" });
            DropIndex("dbo.Tickets", new[] { "ConcessionId" });
            AlterColumn("dbo.Tickets", "LineId", c => c.Int());
            AlterColumn("dbo.Tickets", "ConcessionId", c => c.Int());
            RenameColumn(table: "dbo.Tickets", name: "LineId", newName: "Line_LineId");
            RenameColumn(table: "dbo.Tickets", name: "ConcessionId", newName: "Concession_ConcessionId");
            CreateIndex("dbo.Tickets", "Line_LineId");
            CreateIndex("dbo.Tickets", "ConcessionId_ConcessionId");
            CreateIndex("dbo.Tickets", "Concession_ConcessionId");
            AddForeignKey("dbo.Tickets", "Line_LineId", "dbo.Lines", "LineId");
            AddForeignKey("dbo.Tickets", "Concession_ConcessionId", "dbo.Concessions", "ConcessionId");
            AddForeignKey("dbo.Tickets", "ConcessionId_ConcessionId", "dbo.Concessions", "ConcessionId");
        }
    }
}
