using Microsoft.EntityFrameworkCore.Migrations;

namespace Network.Infrastructure.Migrations
{
    public partial class AddedFieldNameInBirthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Birthdays",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Birthdays");
        }
    }
}
