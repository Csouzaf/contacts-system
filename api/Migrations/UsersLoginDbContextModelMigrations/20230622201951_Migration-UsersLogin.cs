using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.UsersLoginDbContextModelMigrations
{
    public partial class MigrationUsersLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersLogin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usersLogin_Email",
                table: "usersLogin",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersLogin");
        }
    }
}
