namespace IChatYou.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Bannings",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        BannedUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.BannedUserId })
                .ForeignKey("dbo.Users", t => t.BannedUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BannedUserId);
            
            CreateTable(
                "dbo.Favorits",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FavoritUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.FavoritUserId })
                .ForeignKey("dbo.Users", t => t.FavoritUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FavoritUserId);
            
            CreateTable(
                "dbo.Limits",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MessageSwitches",
                c => new
                    {
                        SenderUserId = c.String(nullable: false, maxLength: 128),
                        TargetUserId = c.String(nullable: false, maxLength: 128),
                        MessageId = c.Int(nullable: false),
                        IsReaded = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.SenderUserId, t.TargetUserId, t.MessageId })
                .ForeignKey("dbo.BaseMessages", t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.SenderUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.TargetUserId)
                .Index(t => t.SenderUserId)
                .Index(t => t.TargetUserId)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.BaseMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Sent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        MessageTemplate = c.String(),
                        Level = c.String(maxLength: 128),
                        TimeStamp = c.DateTime(nullable: false),
                        Exception = c.String(),
                        Properties = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 64),
                        Value = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MessageSwitches", "TargetUserId", "dbo.Users");
            DropForeignKey("dbo.MessageSwitches", "SenderUserId", "dbo.Users");
            DropForeignKey("dbo.MessageSwitches", "MessageId", "dbo.BaseMessages");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Limits", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorits", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorits", "FavoritUserId", "dbo.Users");
            DropForeignKey("dbo.Bannings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bannings", "BannedUserId", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.MessageSwitches", new[] { "MessageId" });
            DropIndex("dbo.MessageSwitches", new[] { "TargetUserId" });
            DropIndex("dbo.MessageSwitches", new[] { "SenderUserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Limits", new[] { "UserId" });
            DropIndex("dbo.Favorits", new[] { "FavoritUserId" });
            DropIndex("dbo.Favorits", new[] { "UserId" });
            DropIndex("dbo.Bannings", new[] { "BannedUserId" });
            DropIndex("dbo.Bannings", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropTable("dbo.Settings");
            DropTable("dbo.Logs");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.BaseMessages");
            DropTable("dbo.MessageSwitches");
            DropTable("dbo.UserLogins");
            DropTable("dbo.Limits");
            DropTable("dbo.Favorits");
            DropTable("dbo.Bannings");
            DropTable("dbo.Users");
            DropTable("dbo.UserClaims");
        }
    }
}
