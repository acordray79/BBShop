namespace BBShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BBShopAdmin", "CustomerID", c => c.String());
            AddColumn("dbo.BBShopAdmin", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BBShopAdmin", "ApplicationUser_Id");
            AddForeignKey("dbo.BBShopAdmin", "ApplicationUser_Id", "dbo.ApplicationUser", "Id");
            DropColumn("dbo.BBShopAdmin", "OwnerID");
            DropColumn("dbo.BBShopAdmin", "AdminName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BBShopAdmin", "AdminName", c => c.String(nullable: false));
            AddColumn("dbo.BBShopAdmin", "OwnerID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.BBShopAdmin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.BBShopAdmin", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.BBShopAdmin", "ApplicationUser_Id");
            DropColumn("dbo.BBShopAdmin", "CustomerID");
        }
    }
}
