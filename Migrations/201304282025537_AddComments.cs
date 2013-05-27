namespace frontendplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Website = c.String(),
                        Comment = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        BlogPostModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPostModels", t => t.BlogPostModel_ID)
                .Index(t => t.BlogPostModel_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CommentModels", new[] { "BlogPostModel_ID" });
            DropForeignKey("dbo.CommentModels", "BlogPostModel_ID", "dbo.BlogPostModels");
            DropTable("dbo.CommentModels");
        }
    }
}
