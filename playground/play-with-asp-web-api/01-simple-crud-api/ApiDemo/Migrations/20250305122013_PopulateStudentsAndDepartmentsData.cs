using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class PopulateStudentsAndDepartmentsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed Departments table with software development-related departments
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "Capacity" },
                values: new object[,]
                {
                    { 1, "Software Engineering", 60 },
                    { 2, "Quality Assurance & Testing", 40 },
                    { 3, "DevOps", 35 },
                    { 4, "Cybersecurity", 30 },
                    { 5, "Data Science", 50 }
                });

            // Seed Students table
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Name", "Age", "DeptId" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", 20, 1 }, // Software Engineering
                    { 2, "Bob Smith", 22, 2 },    // Quality Assurance & Testing
                    { 3, "Charlie Brown", 19, 3 }, // DevOps
                    { 4, "Diana Lee", 21, 4 },     // Cybersecurity
                    { 5, "Eve Davis", 23, 5 }      // Data Science
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seeded data from Students table
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Remove seeded data from Departments table
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}
