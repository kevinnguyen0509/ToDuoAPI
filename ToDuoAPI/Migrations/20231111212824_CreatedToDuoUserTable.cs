using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDuoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedToDuoUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDuoUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartnerId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FriendOne = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendTwo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendThree = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendFour = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendFive = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendSix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendSeven = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendEight = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendNine = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FriendTen = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDuoUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDuoUsers");
        }
    }
}
