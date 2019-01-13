namespace Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        PackageID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Package", t => t.PackageID, cascadeDelete: true)
                .Index(t => t.PackageID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        PackageID = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PackageID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Qnt = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Package", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetails", "PackageID", "dbo.Package");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Order");
            DropIndex("dbo.Package", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "PackageID" });
            DropTable("dbo.Product");
            DropTable("dbo.Package");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetails");
        }
    }
}
