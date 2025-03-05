using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class EnforceForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Enable foreign key enforcement
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Disable foreign key enforcement (optional)
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;");
        }
    }
}
