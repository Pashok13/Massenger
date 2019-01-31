namespace Massenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RecepientId = c.Int(nullable: false),
                        DateOfSend = c.DateTime(nullable: false),
                        TimeOfSend = c.DateTime(nullable: false),
                        TextMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recepients", t => t.RecepientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RecepientId);
            
            CreateTable(
                "dbo.Recepients",
                c => new
                    {
                        RecepientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.RecepientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        UserPhone = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "RecepientId", "dbo.Recepients");
            DropIndex("dbo.Messages", new[] { "RecepientId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Recepients");
            DropTable("dbo.Messages");
        }
    }
}
