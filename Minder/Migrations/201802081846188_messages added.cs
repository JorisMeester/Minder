namespace Minder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TimeSent = c.DateTime(nullable: false),
                        Subject = c.String(),
                        Content = c.String(),
                        IsEdited = c.Boolean(nullable: false),
                        Receiver_Id = c.String(nullable: false, maxLength: 128),
                        Sender_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Receiver_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id, cascadeDelete: false)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropTable("dbo.Messages");
        }
    }
}
