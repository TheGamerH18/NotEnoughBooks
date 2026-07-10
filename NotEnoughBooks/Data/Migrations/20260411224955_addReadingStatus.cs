using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotEnoughBooks.Migrations
{
    /// <inheritdoc />
    public partial class addReadingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadingStatus",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingStatus",
                table: "Books");
        }
    }
}
