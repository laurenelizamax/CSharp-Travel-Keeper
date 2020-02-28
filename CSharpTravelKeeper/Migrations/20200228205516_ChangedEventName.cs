using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpTravelKeeper.Migrations
{
    public partial class ChangedEventName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.CreateTable(
                name: "ActivityEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ActivityWebsite = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityEvent_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityEvent_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6650d1d8-c918-4e11-bfcc-81c0c236918d", "AQAAAAEAACcQAAAAELX9qjXwBavugwjTuKMoj5S172UVoLx+oSXaqFYxZPgLu6MBw+8ZIMe5LaVO9ygWrA==" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEvent_ApplicationUserId",
                table: "ActivityEvent",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEvent_CityId",
                table: "ActivityEvent",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityEvent");

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62187800-b2ad-46a1-934d-72578fbd7eca", "AQAAAAEAACcQAAAAEF4eedLZkT+UWKLVnU9ezZdrdr783txfyjUlKcPg3KsGIMMm59S47jMeXlGB97LziA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Event_ApplicationUserId",
                table: "Event",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CityId",
                table: "Event",
                column: "CityId");
        }
    }
}
