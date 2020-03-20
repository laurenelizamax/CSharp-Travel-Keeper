using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpTravelKeeper.Migrations
{
    public partial class activeBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Trip",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "City",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2ccf4696-ac17-48bf-89a9-16d57d2110f9", "AQAAAAEAACcQAAAAEFqoIGTZcR7GX+W9sAsoI4wCQ+Vczz7SyfCZbcRJKl7JNHgHYhQEYH9MJjpJZoRflw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "City");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6650d1d8-c918-4e11-bfcc-81c0c236918d", "AQAAAAEAACcQAAAAELX9qjXwBavugwjTuKMoj5S172UVoLx+oSXaqFYxZPgLu6MBw+8ZIMe5LaVO9ygWrA==" });
        }
    }
}
