namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddivisimodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Division",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateDate = c.DateTimeOffset(precision: 7),
                        DeleteDate = c.DateTimeOffset(precision: 7),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_Division", "DepartmentId", "dbo.TB_M_Department");
            DropIndex("dbo.TB_M_Division", new[] { "DepartmentId" });
            DropTable("dbo.TB_M_Division");
        }
    }
}
