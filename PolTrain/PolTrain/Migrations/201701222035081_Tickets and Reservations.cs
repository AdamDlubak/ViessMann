namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsandReservations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservations", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Reservations", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            DropColumn("dbo.Reservations", "UserId_FirstName");
            DropColumn("dbo.Reservations", "UserId_LastName");
            DropColumn("dbo.Reservations", "UserId_PhoneNumber");
            DropColumn("dbo.Reservations", "UserId_Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "UserId_Email", c => c.String());
            AddColumn("dbo.Reservations", "UserId_PhoneNumber", c => c.String());
            AddColumn("dbo.Reservations", "UserId_LastName", c => c.String());
            AddColumn("dbo.Reservations", "UserId_FirstName", c => c.String());
            RenameIndex(table: "dbo.Reservations", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Reservations", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
