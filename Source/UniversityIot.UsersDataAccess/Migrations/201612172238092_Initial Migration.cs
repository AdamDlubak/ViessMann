namespace UniversityIot.UsersDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGateways", "User_Id", "dbo.Users");
            DropIndex("dbo.UserGateways", new[] { "User_Id" });
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "CustomerNumber", c => c.String());
            DropTable("dbo.UserGateways");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGateways",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GatewaySerial = c.String(),
                        AccessType = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Users", "CustomerNumber", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.UserGateways", "User_Id");
            AddForeignKey("dbo.UserGateways", "User_Id", "dbo.Users", "Id");
        }
    }
}
