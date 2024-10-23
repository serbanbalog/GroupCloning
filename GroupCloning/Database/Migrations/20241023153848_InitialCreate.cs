using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCloning.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNumber = table.Column<int>(type: "int", nullable: false),
                    IdentifierInGroup = table.Column<int>(type: "int", nullable: false),
                    Prop1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prop2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prop3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupNumber_IdentifierInGroup",
                table: "Groups",
                columns: new[] { "GroupNumber", "IdentifierInGroup" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
