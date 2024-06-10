using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KanbanBoard.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BoardsdeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bcbdb94-0a6a-4208-bbc4-50df38393860");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f4b87c7-107d-464b-959a-e6f14357020e");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Boards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09177b66-76f6-4117-94c9-b9cbeaf12650", "5a481f66-8ebe-4d71-983f-217f646af715", "Admin", "ADMIN" },
                    { "2d61f3ad-b502-433f-aee0-76fbf60a91cc", "45ac3444-24d7-400b-82fd-796288c5c35a", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_UserId",
                table: "Boards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_UserId",
                table: "Boards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_UserId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_UserId",
                table: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09177b66-76f6-4117-94c9-b9cbeaf12650");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d61f3ad-b502-433f-aee0-76fbf60a91cc");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Boards");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0bcbdb94-0a6a-4208-bbc4-50df38393860", "c69196ca-d3ba-4534-9bfb-1af0735c6ede", "Admin", "ADMIN" },
                    { "0f4b87c7-107d-464b-959a-e6f14357020e", "170e40b5-ba43-4ed4-afb7-d9eefa1dd80b", "User", "USER" }
                });
        }
    }
}
