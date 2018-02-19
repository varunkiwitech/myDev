namespace Kauffman.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssessmentQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        QuestionText = c.String(),
                        ParentQuestionId = c.Guid(),
                        QuestionType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionTypes", t => t.QuestionType_Id)
                .Index(t => t.QuestionType_Id);
            
            CreateTable(
                "dbo.QuestionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Type = c.String(),
                        MinValue = c.Double(nullable: false),
                        MaxValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SubscriptionName = c.String(),
                        Description = c.String(),
                        DiscountDescription = c.String(),
                        DiscountDuration = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        SubscriptionType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubscriptionTypes", t => t.SubscriptionType_Id)
                .Index(t => t.SubscriptionType_Id);
            
            CreateTable(
                "dbo.SubscriptionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Type = c.String(),
                        DurationMonths = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAssessmentAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Answer = c.String(),
                        Question_Id = c.Guid(),
                        UserAssementStatus_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssessmentQuestions", t => t.Question_Id)
                .ForeignKey("dbo.UserAssessmentStatus", t => t.UserAssementStatus_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.UserAssementStatus_Id);
            
            CreateTable(
                "dbo.UserAssessmentStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AssessmentDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SubscriptionStartDate = c.DateTime(nullable: false),
                        SubscriptionEndDate = c.DateTime(nullable: false),
                        IsSubscriptionActive = c.Boolean(nullable: false),
                        SubscriptionUpdateDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        UserCurrentSubscription_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Subscriptions", t => t.UserCurrentSubscription_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserCurrentSubscription_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubscriptions", "UserCurrentSubscription_Id", "dbo.Subscriptions");
            DropForeignKey("dbo.UserSubscriptions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAssessmentAnswers", "UserAssementStatus_Id", "dbo.UserAssessmentStatus");
            DropForeignKey("dbo.UserAssessmentStatus", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAssessmentAnswers", "Question_Id", "dbo.AssessmentQuestions");
            DropForeignKey("dbo.Subscriptions", "SubscriptionType_Id", "dbo.SubscriptionTypes");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.AssessmentQuestions", "QuestionType_Id", "dbo.QuestionTypes");
            DropIndex("dbo.UserSubscriptions", new[] { "UserCurrentSubscription_Id" });
            DropIndex("dbo.UserSubscriptions", new[] { "User_Id" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserAssessmentStatus", new[] { "User_Id" });
            DropIndex("dbo.UserAssessmentAnswers", new[] { "UserAssementStatus_Id" });
            DropIndex("dbo.UserAssessmentAnswers", new[] { "Question_Id" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriptionType_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.AssessmentQuestions", new[] { "QuestionType_Id" });
            DropTable("dbo.UserSubscriptions");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserAssessmentStatus");
            DropTable("dbo.UserAssessmentAnswers");
            DropTable("dbo.SubscriptionTypes");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.QuestionTypes");
            DropTable("dbo.AssessmentQuestions");
        }
    }
}
