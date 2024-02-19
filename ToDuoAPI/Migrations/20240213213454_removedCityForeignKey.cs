using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedCityForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_ToDuoCity_City",
                table: "Adventures");

            migrationBuilder.DropTable(
                name: "ToDuoCity");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_City",
                table: "Adventures");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Adventures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "City",
                table: "Adventures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ToDuoCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDuoCity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_City",
                table: "Adventures",
                column: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_ToDuoCity_City",
                table: "Adventures",
                column: "City",
                principalTable: "ToDuoCity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
