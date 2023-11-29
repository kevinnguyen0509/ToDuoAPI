using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedFKState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_ToDuoStatesID",
                table: "Adventures",
                column: "ToDuoStatesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_ToDuoStates_ToDuoStatesID",
                table: "Adventures",
                column: "ToDuoStatesID",
                principalTable: "ToDuoStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_ToDuoStates_ToDuoStatesID",
                table: "Adventures");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_ToDuoStatesID",
                table: "Adventures");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Adventures",
                type: "int",
                nullable: true);

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
    }
}
