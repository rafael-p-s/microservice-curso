using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class users_table_creation : Migration
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
                    POSTAL_CODE = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
