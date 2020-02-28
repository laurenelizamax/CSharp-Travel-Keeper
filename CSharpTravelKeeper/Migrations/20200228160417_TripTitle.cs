using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpTravelKeeper.Migrations
{
    public partial class TripTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Trip");

            migrationBuilder.AddColumn<string>(
                name: "TripTitle",
                table: "Trip",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62187800-b2ad-46a1-934d-72578fbd7eca", "AQAAAAEAACcQAAAAEF4eedLZkT+UWKLVnU9ezZdrdr783txfyjUlKcPg3KsGIMMm59S47jMeXlGB97LziA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripTitle",
                table: "Trip");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8100153c-03cb-4ce0-af7a-a8639e446e6b", "AQAAAAEAACcQAAAAEEyzRPvxibuZme19ORpgTkC7fkgfshh5g1BYLF8fg4NZ28Fq+Fw/JmO11+eVBq9ZPw==" });
        }
    }
}
