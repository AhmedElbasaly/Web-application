namespace my_project_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyForJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applyforjobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        massage = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JobId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applyforjobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applyforjobs", "JobId", "dbo.Jobs");
            DropIndex("dbo.Applyforjobs", new[] { "UserId" });
            DropIndex("dbo.Applyforjobs", new[] { "JobId" });
            DropTable("dbo.Applyforjobs");
        }
    }
}
