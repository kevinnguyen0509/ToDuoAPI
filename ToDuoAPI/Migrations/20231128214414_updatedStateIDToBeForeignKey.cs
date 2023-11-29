using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedStateIDToBeForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Adventures");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Adventures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToDuoStatesID",
                table: "Adventures",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_StateID",
                table: "Adventures",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_ToDuoStates_StateID",
                table: "Adventures",
                column: "StateID",
                principalTable: "ToDuoStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_ToDuoStates_StateID",
                table: "Adventures");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_StateID",
                table: "Adventures");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Adventures");

            migrationBuilder.DropColumn(
                name: "ToDuoStatesID",
                table: "Adventures");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Adventures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
