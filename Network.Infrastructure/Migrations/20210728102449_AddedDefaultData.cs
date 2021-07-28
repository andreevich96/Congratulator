using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Network.Infrastructure.Migrations
{
    public partial class AddedDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Birthdays",
                columns: new[] { "Id", "DateOfBirth", "Name", "Note", "Photo", "Relationship" },
                values: new object[] { 100, new DateTime(1991, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петров Петр Петрович", "Не забыть поздравить хоть в этот раз", null, "Брат" });

            migrationBuilder.InsertData(
                table: "Birthdays",
                columns: new[] { "Id", "DateOfBirth", "Name", "Note", "Photo", "Relationship" },
                values: new object[] { 101, new DateTime(1986, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петя Александров", "Новая заметка", null, "Брат" });

            migrationBuilder.InsertData(
                table: "Birthdays",
                columns: new[] { "Id", "DateOfBirth", "Name", "Note", "Photo", "Relationship" },
                values: new object[] { 102, new DateTime(1956, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алексей Иванов Александрович", "Заметка 3", null, "Друг" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Birthdays",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Birthdays",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Birthdays",
                keyColumn: "Id",
                keyValue: 102);
        }
    }
}
