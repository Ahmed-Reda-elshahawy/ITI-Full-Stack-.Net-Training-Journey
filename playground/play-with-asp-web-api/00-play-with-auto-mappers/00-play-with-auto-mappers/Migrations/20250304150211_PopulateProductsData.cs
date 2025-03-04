using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _00_play_with_auto_mappers.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProductsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Products (Name, Price, CategoryId, CreatedAt, UpdatedAt)
                VALUES 
                    ('Smartphone', 599.99, 1, DATETIME('now'), DATETIME('now')),
                    ('Laptop', 1299.99, 1, DATETIME('now'), DATETIME('now')),
                    ('Headphones', 199.99, 1, DATETIME('now'), DATETIME('now')),
                    ('T-Shirt', 19.99, 2, DATETIME('now'), DATETIME('now')),
                    ('Jeans', 49.99, 2, DATETIME('now'), DATETIME('now')),
                    ('Novel', 14.99, 3, DATETIME('now'), DATETIME('now')),
                    ('Cookbook', 24.99, 3, DATETIME('now'), DATETIME('now')),
                    ('Blender', 79.99, 4, DATETIME('now'), DATETIME('now')),
                    ('Toaster', 39.99, 4, DATETIME('now'), DATETIME('now')),
                    ('Drone', 299.99, 5, DATETIME('now'), DATETIME('now'));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products;");
        }
    }
}
