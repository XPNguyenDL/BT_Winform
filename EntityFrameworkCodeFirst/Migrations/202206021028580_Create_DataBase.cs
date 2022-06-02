namespace EntityFrameworkCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_DataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaLop = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lops", t => t.MaLop)
                .Index(t => t.MaLop);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SinhViens", "MaLop", "dbo.Lops");
            DropIndex("dbo.SinhViens", new[] { "MaLop" });
            DropTable("dbo.SinhViens");
            DropTable("dbo.Lops");
        }
    }
}
