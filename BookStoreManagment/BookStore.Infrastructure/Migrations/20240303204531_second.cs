using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "BookName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Books",
                newName: "BookName");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookName",
                table: "Orders",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BookName",
                table: "Books",
                newName: "Name");
        }
    }
}
