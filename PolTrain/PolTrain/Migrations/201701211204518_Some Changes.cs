namespace PolTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tracks", "Length", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tracks", "Length", c => c.Single(nullable: false));
        }
    }
}
