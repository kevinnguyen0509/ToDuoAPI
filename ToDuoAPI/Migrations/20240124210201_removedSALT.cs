using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedSALT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "05a99a8a-2034-413a-95f1-628963cd180d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1058f6f0-2847-49f5-9efc-c54a8a2f9e25");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d323f58-9854-42ac-888a-33375537dd73", null, "User", "USER" },
                    { "cac00dc5-9236-4abb-a230-826565173c9e", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2d323f58-9854-42ac-888a-33375537dd73");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "cac00dc5-9236-4abb-a230-826565173c9e");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05a99a8a-2034-413a-95f1-628963cd180d", null, "User", "USER" },
                    { "1058f6f0-2847-49f5-9efc-c54a8a2f9e25", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
