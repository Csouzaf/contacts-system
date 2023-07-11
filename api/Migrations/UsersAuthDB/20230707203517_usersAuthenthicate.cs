using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations.UsersAuthDB
{
    public partial class usersAuthenthicate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersAuthenthicate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersAuthenthicate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthenthicateUserEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAuthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenthicateUserEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenthicateUserEmail_usersAuthenthicate_UserAuthId",
                        column: x => x.UserAuthId,
                        principalTable: "usersAuthenthicate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenthicateUserEmail_UserAuthId",
                table: "AuthenthicateUserEmail",
                column: "UserAuthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usersAuthenthicate_Email",
                table: "usersAuthenthicate",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenthicateUserEmail");

            migrationBuilder.DropTable(
                name: "usersAuthenthicate");
        }
    }
}
