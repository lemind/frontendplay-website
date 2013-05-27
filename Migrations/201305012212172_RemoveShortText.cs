namespace frontendplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveShortText : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BlogPostModels", "ShortText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPostModels", "ShortText", c => c.String(nullable: false));
        }
    }
}
