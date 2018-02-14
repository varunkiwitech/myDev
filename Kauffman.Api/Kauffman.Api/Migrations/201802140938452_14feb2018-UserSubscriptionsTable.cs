namespace Kauffman.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14feb2018UserSubscriptionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.String(),
                        SubscriptionId = c.String(),
                        SubscriptionStartDate = c.DateTime(nullable: false),
                        SubscriptionEndDate = c.DateTime(nullable: false),
                        IsSubscriptionActive = c.Boolean(nullable: false),
                        SubscriptionUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSubscriptions");
        }
    }
}
