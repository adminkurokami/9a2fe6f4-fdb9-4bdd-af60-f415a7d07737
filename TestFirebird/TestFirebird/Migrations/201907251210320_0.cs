namespace TestFirebird.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stdt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        UniversityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Unv", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Unv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stdt", "UniversityId", "dbo.Unv");
            DropIndex("dbo.Stdt", new[] { "UniversityId" });
            DropTable("dbo.Unv");
            DropTable("dbo.Stdt");
        }
    }
}
