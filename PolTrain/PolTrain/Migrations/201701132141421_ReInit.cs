namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concessions",
                c => new
                    {
                        ConcessionId = c.Int(nullable: false, identity: true),
                        Discount = c.Int(nullable: false),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.ConcessionId);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineId = c.Int(nullable: false, identity: true),
                        TrackId = c.Int(nullable: false),
                        Departure = c.DateTime(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        ReservedSeatsAmt = c.Int(nullable: false),
                        Timetable_TimetableId = c.Int(),
                    })
                .PrimaryKey(t => t.LineId)
                .ForeignKey("dbo.Tracks", t => t.TrackId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.Timetable_TimetableId)
                .Index(t => t.TrackId)
                .Index(t => t.Timetable_TimetableId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        StartStationId = c.Int(nullable: false),
                        EndStationId = c.Int(nullable: false),
                        TrackName = c.String(),
                        Length = c.Single(nullable: false),
                        SeatsAmt = c.Int(nullable: false),
                        Diner = c.Boolean(nullable: false),
                        NormalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JourneyTime = c.Time(nullable: false, precision: 7),
                        ClassLevel = c.Int(nullable: false),
                        EndStation_StationId = c.Int(),
                        StartStation_StationId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackId)
                .ForeignKey("dbo.Stations", t => t.EndStation_StationId)
                .ForeignKey("dbo.Stations", t => t.StartStation_StationId)
                .Index(t => t.EndStation_StationId)
                .Index(t => t.StartStation_StationId);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.StationId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        IsPay = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        UserId_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Users", t => t.UserId_UserId)
                .Index(t => t.UserId_UserId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        SeatsAmt = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Concession_ConcessionId = c.Int(),
                        Line_LineId = c.Int(),
                        Reservation_ReservationId = c.Int(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Concessions", t => t.Concession_ConcessionId)
                .ForeignKey("dbo.Lines", t => t.Line_LineId)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationId)
                .Index(t => t.Concession_ConcessionId)
                .Index(t => t.Line_LineId)
                .Index(t => t.Reservation_ReservationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        CodeAndCity = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        TimetableId = c.Int(nullable: false, identity: true),
                        TrackId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        SeatsQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimetableId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lines", "Timetable_TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.Reservations", "UserId_UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "Reservation_ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Tickets", "Line_LineId", "dbo.Lines");
            DropForeignKey("dbo.Tickets", "Concession_ConcessionId", "dbo.Concessions");
            DropForeignKey("dbo.Lines", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "StartStation_StationId", "dbo.Stations");
            DropForeignKey("dbo.Tracks", "EndStation_StationId", "dbo.Stations");
            DropIndex("dbo.Tickets", new[] { "Reservation_ReservationId" });
            DropIndex("dbo.Tickets", new[] { "Line_LineId" });
            DropIndex("dbo.Tickets", new[] { "Concession_ConcessionId" });
            DropIndex("dbo.Reservations", new[] { "UserId_UserId" });
            DropIndex("dbo.Tracks", new[] { "StartStation_StationId" });
            DropIndex("dbo.Tracks", new[] { "EndStation_StationId" });
            DropIndex("dbo.Lines", new[] { "Timetable_TimetableId" });
            DropIndex("dbo.Lines", new[] { "TrackId" });
            DropTable("dbo.Timetables");
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.Reservations");
            DropTable("dbo.Stations");
            DropTable("dbo.Tracks");
            DropTable("dbo.Lines");
            DropTable("dbo.Concessions");
        }
    }
}
