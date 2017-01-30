namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsandReservations4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "Reservation_ReservationId", "dbo.Reservations");
            DropIndex("dbo.Tickets", new[] { "Reservation_ReservationId" });
            RenameColumn(table: "dbo.Tickets", name: "Reservation_ReservationId", newName: "ReservationId");
            AlterColumn("dbo.Tickets", "ReservationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "ReservationId");
            AddForeignKey("dbo.Tickets", "ReservationId", "dbo.Reservations", "ReservationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Tickets", new[] { "ReservationId" });
            AlterColumn("dbo.Tickets", "ReservationId", c => c.Int());
            RenameColumn(table: "dbo.Tickets", name: "ReservationId", newName: "Reservation_ReservationId");
            CreateIndex("dbo.Tickets", "Reservation_ReservationId");
            AddForeignKey("dbo.Tickets", "Reservation_ReservationId", "dbo.Reservations", "ReservationId");
        }
    }
}
