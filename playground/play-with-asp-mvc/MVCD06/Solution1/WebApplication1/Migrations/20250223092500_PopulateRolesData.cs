using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PopulateRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert roles into the Roles table
            migrationBuilder.Sql(@"
                INSERT INTO Roles(Name)
                VALUES ('Admin'),
                       ('Student'),
                       ('Teacher');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Truncate the entire Roles table
            migrationBuilder.Sql("TRUNCATE TABLE Roles;");
        }
    }
}
