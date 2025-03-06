using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDepartmentsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO 
                    Departments (Name, Capacity)
                VALUES
                    ('Human Resources', 25),
                    ('Finance', 40),
                    ('Engineering', 50),
                    ('Marketing', 30),
                    ('IT Support', 45)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM 
                    Departments
                WHERE 
                    Name IN ('Human Resources', 'Finance', 'Engineering', 'Marketing', 'IT Support')");
        }
    }
}
