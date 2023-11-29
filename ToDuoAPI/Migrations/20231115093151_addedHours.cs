using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hours",
                table: "Adventures",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Adventures");
        }
    }
}
