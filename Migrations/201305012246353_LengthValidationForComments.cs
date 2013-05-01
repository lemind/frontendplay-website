namespace frontendplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LengthValidationForComments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommentModels", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.CommentModels", "Email", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.CommentModels", "Website", c => c.String(maxLength: 100));
            AlterColumn("dbo.CommentModels", "Comment", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentModels", "Comment", c => c.String(nullable: false));
            AlterColumn("dbo.CommentModels", "Website", c => c.String());
            AlterColumn("dbo.CommentModels", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.CommentModels", "Name", c => c.String(nullable: false));
        }
    }
}
