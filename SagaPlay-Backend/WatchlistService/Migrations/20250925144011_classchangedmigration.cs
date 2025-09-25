using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchlistService.Migrations
{
    /// <inheritdoc />
    public partial class classchangedmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchListItems_WatchListId",
                table: "WatchListItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WatchLists",
                newName: "WatchListId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchListItems_WatchListId_ContentItemId",
                table: "WatchListItems",
                columns: new[] { "WatchListId", "ContentItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchListItems_WatchListId_ContentItemId",
                table: "WatchListItems");

            migrationBuilder.RenameColumn(
                name: "WatchListId",
                table: "WatchLists",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WatchListItems_WatchListId",
                table: "WatchListItems",
                column: "WatchListId");
        }
    }
}
