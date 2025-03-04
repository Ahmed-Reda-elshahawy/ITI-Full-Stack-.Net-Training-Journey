using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _00_play_with_auto_mappers.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategoriesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Categories (Name, CreatedAt)
                VALUES 
                    ('Electronics', DATETIME('now')),
                    ('Clothing', DATETIME('now')),
                    ('Books', DATETIME('now')),
                    ('Home & Kitchen', DATETIME('now')),
                    ('Toys & Games', DATETIME('now')),
                    ('Sports & Outdoors', DATETIME('now')),
                    ('Beauty & Personal Care', DATETIME('now')),
                    ('Health & Household', DATETIME('now')),
                    ('Automotive', DATETIME('now')),
                    ('Tools & Home Improvement', DATETIME('now'));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories;");
        }
    }
}
