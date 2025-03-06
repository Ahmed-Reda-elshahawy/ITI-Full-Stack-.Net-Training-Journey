using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class PopulateUsersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [Users] (Name, Address)
                VALUES 
                ('John Doe', '123 Main St'),
                ('Jane Smith', '456 Elm St'),
                ('Alice Johnson', '789 Oak St'),
                ('Bob Brown', '321 Pine St'),
                ('Charlie Davis', '654 Maple St');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE [Users]");
        }
    }
}
