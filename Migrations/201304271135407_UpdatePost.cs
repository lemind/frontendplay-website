namespace frontendplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPostModels", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.BlogPostModels", "ShortText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPostModels", "ShortText", c => c.String());
            DropColumn("dbo.BlogPostModels", "Author");
        }
    }
}
