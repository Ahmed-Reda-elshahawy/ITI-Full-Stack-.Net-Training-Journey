using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PopulateStudentsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO 
                    Students (Name, DeptId)
                VALUES
                    ('Alice Johnson', 1),  -- Human Resources
                    ('Bob Smith', 2),      -- Finance
                    ('Charlie Davis', 3),  -- Engineering
                    ('Diana Prince', 4),   -- Marketing
                    ('Ethan Hunt', 5)      -- IT Support
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM 
                    Students
                WHERE 
                    Name IN ('Alice Johnson', 'Bob Smith', 'Charlie Davis', 'Diana Prince', 'Ethan Hunt')
            ");
        }
    }
}
