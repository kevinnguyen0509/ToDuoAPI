using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedCategoryForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert a default category if it doesn't exist
            migrationBuilder.Sql("INSERT INTO ToDuoCategories (Name) SELECT 'Default Category' WHERE NOT EXISTS (SELECT * FROM ToDuoCategories WHERE Name = 'Default Category')");

            // Assuming the default category is the first one and has ID = 1
            // This is a simplification and might not be true in your actual database
            // Ideally, you should retrieve this ID programmatically
            var defaultCategoryId = 1;

            migrationBuilder.AddColumn<int>(
                name: "ToDuoCategoryId",
                table: "Adventures",
                nullable: false,
                defaultValue: defaultCategoryId);

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_ToDuoCategoryId",
                table: "Adventures",
                column: "ToDuoCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_ToDuoCategories_ToDuoCategoryId",
                table: "Adventures",
                column: "ToDuoCategoryId",
                principalTable: "ToDuoCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_ToDuoCategories_ToDuoCategoryId",
                table: "Adventures");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_ToDuoCategoryId",
                table: "Adventures");

            migrationBuilder.DropColumn(
                name: "ToDuoCategoryId",
                table: "Adventures");
        }
    }
}
