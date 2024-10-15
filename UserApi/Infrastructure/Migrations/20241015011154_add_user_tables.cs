using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: false),
                    AGE = table.Column<int>(type: "INTEGER", nullable: false),
                    EMAIL = table.Column<string>(type: "TEXT", nullable: false),
                    ADDRESS = table.Column<string>(type: "TEXT", nullable: false),
                    CITY = table.Column<string>(type: "TEXT", nullable: false),
                    COUNTRY = table.Column<string>(type: "TEXT", nullable: false),
                    POSTAL_CODE = table.Column<string>(type: "TEXT", nullable: false),
                    API_KEY = table.Column<string>(type: "TEXT", nullable: false),
                    IS_SYS_ADMIN = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    USER_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    NAME = table.Column<string>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ROLES_USERS_UserId1",
                        column: x => x.UserId1,
                        principalTable: "USERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_USER_ROLE_USER",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_USER_ID",
                table: "USER_ROLES",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_UserId1",
                table: "USER_ROLES",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_ROLES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
