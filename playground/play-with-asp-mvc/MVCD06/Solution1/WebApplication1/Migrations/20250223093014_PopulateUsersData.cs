using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
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
            // Insert sample users into the Users table
            migrationBuilder.Sql(@"
                INSERT INTO Users (Name, Email, Password)
                VALUES 
                    ('John Doe', 'john.doe@example.com', 'password123'),
                    ('Jane Smith', 'jane.smith@example.com', 'securepass456'),
                    ('Alice Johnson', 'alice.johnson@example.com', 'alicepass789');
            "
            );

            // Insert associations into the UserRole junction table
            migrationBuilder.Sql(@"
                -- John Doe is an Admin
                INSERT INTO RoleUser(UsersId, RolesId)
                SELECT U.Id, R.Id
                FROM Users U, Roles R
                WHERE U.Email = 'john.doe@example.com' AND R.Name = 'Admin';

                -- Jane Smith is a Student
                INSERT INTO RoleUser(UsersId, RolesId)
                SELECT U.Id, R.Id
                FROM Users U, Roles R
                WHERE U.Email = 'jane.smith@example.com' AND R.Name = 'Student';

                -- Alice Johnson is a Teacher
                INSERT INTO RoleUser(UsersId, RolesId)
                SELECT U.Id, R.Id
                FROM Users U, Roles R
                WHERE U.Email = 'alice.johnson@example.com' AND R.Name = 'Teacher';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Truncate the UserRole junction table
            migrationBuilder.Sql("TRUNCATE TABLE RoleUser;");

            // Truncate the Users table
            migrationBuilder.Sql("TRUNCATE TABLE Users;");
        }
    }
}
