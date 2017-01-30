namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsandReservations2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ReservationValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "ReservationValue");
        }
    }
}
