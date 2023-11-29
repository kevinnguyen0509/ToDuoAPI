using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedCityForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "City",
                table: "Adventures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_ToDuoCity_City",
                table: "Adventures");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_City",
                table: "Adventures");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Adventures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
